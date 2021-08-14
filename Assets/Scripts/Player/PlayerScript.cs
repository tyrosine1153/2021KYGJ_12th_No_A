using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerScript : PersistentSingleton<PlayerScript>
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int IsGround = Animator.StringToHash("IsGround");
    private static readonly int Wall = Animator.StringToHash("Wall");
    private static readonly int JumpUp = Animator.StringToHash("JumpUp");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int Dash1 = Animator.StringToHash("Dash");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("PlayerState")] 
    public bool isGround;
    public bool isWall;
    public bool isSit;
    public bool canControl;
    [SerializeField] private bool isWalking;
    
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxDashCoolTime;
    [SerializeField] private float curDashCool;
    [SerializeField] private float dashPower;
    [SerializeField] private float rollPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float rollingTime;

    [Header("Ground")] 
    [SerializeField] private float groundLength;
    [SerializeField] private GameObject smoke;

    [Header("Wall")] 
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallSpeed;
    [SerializeField] private float wallLength;

    [Header("Spring")] 
    [SerializeField] private float springPower;

    [Header("Rigid")] 
    [SerializeField] private float curVelocityY;

    [Header("Axis")] 
    [SerializeField] private float inputX;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        //데쉬 쿨타임 재기
        if (curDashCool >= 0) curDashCool -= Time.deltaTime;

        if (canControl) Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spring") && _rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.AddForce(new Vector2(0, springPower));
        }
    }

    private void Move()
    {
        // 현재 상태 저장
        var pos = transform.position;
        Debug.DrawRay(new Vector3(pos.x - 0.49f, pos.y - 0.6f, 0), transform.right,
            Color.red);
        isWall = Physics2D.Raycast(pos, new Vector3(inputX, 0, 0), wallLength,
            LayerMask.GetMask("Ground"));
        isGround = Physics2D.Raycast(new Vector3(pos.x - 0.49f, pos.y - 0.6f, 0),
            new Vector3(1, 0, 0), groundLength, LayerMask.GetMask("Ground"));

        if (isWall) curVelocityY = -wallSpeed;
        else curVelocityY = _rigidbody.velocity.y;

        // 입력
        inputX = Input.GetAxisRaw("Horizontal");

        //데쉬 쿨타임이 돌았고 Shift누르면 데쉬
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

        // 앉기
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGround && inputX == 0)
        {
            isSit = true;
            _rigidbody.velocity = new Vector2(0, 0);
        }
        else
        {
            isSit = false;
            _rigidbody.velocity = new Vector2(inputX * moveSpeed, curVelocityY);
        }

        //땅에 있고 스페이스바를 누르면 점프 실행
        if (Input.GetKeyDown(KeyCode.Space) && inputX != 0 && isWall && !isGround)
        {
            StartCoroutine(WallJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            EffectSoundManager.Instance.PlayEffect(5);
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(); //어플리케이션 종료
#endif
        }

        // 애니메이션
        _animator.SetBool(Walk, inputX != 0);
        _animator.SetBool(IsGround, isGround);
        _animator.SetBool(Wall, isWall);
        _animator.SetBool(JumpUp, _rigidbody.velocity.y > 0);
        _animator.SetBool(Sit, isSit);

        // 효과음
        if (inputX != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                EffectSoundManager.Instance.FootWalkStart();
            }

            _spriteRenderer.flipX = inputX < 0;
        }
        else if (isWalking)
        {
            isWalking = false;
            EffectSoundManager.Instance.FootWalkStop();
        }
    }

    private void Jump()
    {
        if (isWall) return;
        Smoke();
        _rigidbody.AddForce(new Vector2(0, jumpPower));
    }

    private IEnumerator Dash()
    {
        curDashCool = maxDashCoolTime;
        _animator.SetBool(Dash1, true);

        canControl = false;

        _rigidbody.velocity = new Vector2(0, 0);
        if (inputX > 0) _rigidbody.velocity = new Vector2(dashPower, 0);
        else _rigidbody.velocity = new Vector2(-dashPower, 0);

        yield return new WaitForSeconds(dashingTime);

        _animator.SetBool(Dash1, false);
        canControl = true;
    }

    private IEnumerator Roll()
    {
        curDashCool = maxDashCoolTime;
        _animator.SetBool(Dash1, true);

        canControl = false;

        _rigidbody.velocity = new Vector2(0, 0);
        if (inputX > 0) _rigidbody.velocity = new Vector2(rollPower, 0);
        else _rigidbody.velocity = new Vector2(-rollPower, 0);

        yield return new WaitForSeconds(rollingTime);

        _animator.SetBool(Dash1, false);
        canControl = true;
    }

    private IEnumerator WallJump()
    {
        canControl = false;

        _rigidbody.AddForce(inputX > 0
            ? new Vector2(-wallJumpForce.x, wallJumpForce.y)
            : new Vector2(wallJumpForce.x, wallJumpForce.y));

        yield return new WaitForSeconds(0.1f);

        canControl = true;
    }

    public void Smoke()
    {
        if (inputX > 0)
            Instantiate(smoke, transform.position, Quaternion.identity);
        else
            Instantiate(smoke, transform.position, Quaternion.identity).transform.GetChild(0)
                .GetComponent<SpriteRenderer>().flipX = true;
    }
}