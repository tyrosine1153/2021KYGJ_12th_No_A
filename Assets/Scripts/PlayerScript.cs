using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] GameManager gameManager;

    [Header("Inspector")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator ani;
    [SerializeField] SpriteRenderer sprite;

    [Header("Time")]
    [SerializeField] float curDashCool;

    [Header("PlayerState")]
    public bool isGround;
    [SerializeField] bool isWall;
    public bool canControl;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxDashCoolTime;
    [SerializeField] float dashPower;
    [SerializeField] float rollPower;
    [SerializeField] float dashingTime;
    [SerializeField] float rollingTime;

    [Header("Ground")]
    [SerializeField] float GroundLength;

    [Header("Wall")]
    [SerializeField] Vector2 wallJumpForce;
    [SerializeField] float wallSpeed;
    [SerializeField] float wallLength;

    [Header("Spring")]
    [SerializeField] float springPower;

    [Header("RIgid")]
    [SerializeField] float curVelocityY;

    [Header("Axis")]
    [SerializeField] float inputX;

    private void Update()
    {
        //데쉬 쿨타임 재기
        if (curDashCool >= 0) curDashCool -= Time.deltaTime;

        //데쉬 쿨타임이 돌았고 Shift누르면 데쉬
        if (curDashCool < 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isGround && inputX != 0) StartCoroutine(Dash());
        if(curDashCool < 0 && Input.GetKeyDown(KeyCode.S) && isGround && inputX != 0) StartCoroutine(Roll());

        if (canControl) Move();

        if (Input.GetKeyDown(KeyCode.Space) && canControl && inputX != 0 && isWall && !isGround) StartCoroutine(WallJump());
        //땅에 있고 스페이스바를 누르면 점프 실행
        else if (Input.GetKeyDown(KeyCode.Space) && canControl && isGround) Jump();


    }

    void Move()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        ani.SetBool("Walk", inputX == 0 ? false : true);
        ani.SetBool("IsGround", isGround);
        ani.SetBool("Wall", isWall);

        Debug.DrawRay(new Vector3(transform.position.x -0.49f, transform.position.y - 0.6f, 0), transform.right, Color.red);
        isWall = Physics2D.Raycast(transform.position, new Vector3(inputX, 0, 0), wallLength, LayerMask.GetMask("Ground"));
        isGround = Physics2D.Raycast(new Vector3(transform.position.x - 0.49f, transform.position.y - 0.6f, 0), new Vector3(1, 0, 0), GroundLength, LayerMask.GetMask("Ground"));

        if (isWall) curVelocityY = -wallSpeed;
        else curVelocityY = rigid.velocity.y;

        if (rigid.velocity.y > 0) ani.SetBool("JumpUp", true);
        else ani.SetBool("JumpUp", false);

        if (inputX < 0) sprite.flipX = true;
        else if (inputX > 0) sprite.flipX = false;

        rigid.velocity = new Vector2(inputX * moveSpeed, curVelocityY);
    }

    void Jump()
    {
        rigid.AddForce(new Vector2(0, jumpPower));
    }

    IEnumerator Dash()
    {
        curDashCool = maxDashCoolTime;
        ani.SetBool("Dash", true);

        canControl = false;

        rigid.velocity = new Vector2(0, 0);
        if (inputX > 0) rigid.velocity = new Vector2(dashPower, 0);
        else rigid.velocity = new Vector2(-dashPower, 0);

        yield return new WaitForSeconds(dashingTime);

        ani.SetBool("Dash", false);
        canControl = true;
    }

    IEnumerator Roll()
    {
        curDashCool = maxDashCoolTime;
        ani.SetBool("Dash", true);

        canControl = false;

        rigid.velocity = new Vector2(0, 0);
        if (inputX > 0) rigid.velocity = new Vector2(rollPower, 0);
        else rigid.velocity = new Vector2(-rollPower, 0);

        yield return new WaitForSeconds(rollingTime);

        ani.SetBool("Dash", false);
        canControl = true;
    }

    IEnumerator WallJump()
    {
        canControl = false;

        if (inputX > 0) rigid.AddForce(new Vector2(-wallJumpForce.x, wallJumpForce.y));
        else rigid.AddForce(new Vector2(wallJumpForce.x, wallJumpForce.y));

        yield return new WaitForSeconds(0.1f);

        canControl = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spring" && rigid.velocity.y < 0)
        {
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, springPower));
        }


        if (collision.tag == "Item")
        {
            //아이템 넣는 함수
        }
    }
}
