using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PersistentSingleton<PlayerScript>
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
    public bool isSit;
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
    [SerializeField] GameObject smoke;

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

    [SerializeField] private bool isWalking;

    private void Update()
    {
        //���� ��Ÿ�� ���
        if (curDashCool >= 0) curDashCool -= Time.deltaTime;

        //���� ��Ÿ���� ���Ұ� Shift������ ����
        if (curDashCool < 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isGround && inputX != 0)
        {
            EffectSoundManager.Instance.PlayEffect(4);
            StartCoroutine(Dash());
        }

        if (curDashCool < 0 && Input.GetKeyDown(KeyCode.LeftShift) && isGround && inputX != 0)
        {
            EffectSoundManager.Instance.PlayEffect(4);
            StartCoroutine(Roll());
        }

        if (canControl) Move();

        if (Input.GetKeyDown(KeyCode.Space) && canControl && inputX != 0 && isWall && !isGround)
        {
            StartCoroutine(WallJump());
        }

        //���� �ְ� �����̽��ٸ� ������ ���� ����
        else if (Input.GetKeyDown(KeyCode.Space) && canControl && isGround)
        {
            EffectSoundManager.Instance.PlayEffect(5);
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); //���ø����̼� ����
#endif
        }
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

        if (inputX != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                EffectSoundManager.Instance.FootWalkStart();
            }

            sprite.flipX = inputX < 0;
        }
        else if(isWalking)
        {
            isWalking = false;
            EffectSoundManager.Instance.FootWalkStop();
        }
        
        if (Input.GetKey(KeyCode.S) && isGround && inputX == 0)
        {
            isSit = true;
            ani.SetBool("Sit", true);
            rigid.velocity = new Vector2(0, 0);
        }
        else
        {
            isSit = false;
            ani.SetBool("Sit", false);
            rigid.velocity = new Vector2(inputX * moveSpeed, curVelocityY);
        }
    }

    void Jump()
    {
        if (isWall) return;
        Smoke();
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

    public void Smoke()
    {
        if (inputX > 0)
            Instantiate(smoke, transform.position, Quaternion.identity);
        else
            Instantiate(smoke, transform.position, Quaternion.identity).transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
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
            //������ �ִ� �Լ�
        }
    }
}