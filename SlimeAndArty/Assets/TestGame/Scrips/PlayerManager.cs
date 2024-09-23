using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : PlayerActive
{
    public static PlayerManager instance {  get; private set; }
    [Header("외부참조")]
    //public GameObject defaultObj; //리스폰장소
    //public GameObject secondObj; //체크포인트후 리스폰
    //public GameObject gameOver; // 게임오버화면
    public List<AudioClip> audioClip; //소리모음집

    [Header("컴포넌트")]
    Rigidbody2D rig2D;
    Camera mainCam;
    protected AudioSource audioSource;
    BoxCollider2D boxCollider;
    Animator animator;

    [Header("움직임")]
    public float jumpHeight; //점프높이
    Vector3 moveDirection; //OnMove의 값을 저장할 벡터3
    float speed = 3.0f; //움직이는 속도
    public bool isjump; //더블점프체크

    [Header("기즈모")]
    public Color collsionColor = Color.red; //기즈모색
    public Vector2 boxClliderSize; //콜라이더의 크기

    [Header("그라운드체크")]
    public bool isCheck = false; //체크포인트 참조
    private bool isGrounded = false; //땅체크
    public Transform groundCheck; //체크할 위치
    public float groundRound = 0.1f; //체크한위치에서 충돌판정할 원의 사이즈
    public LayerMask groundMask; //레이어마스크는 레이어의 정보를 담고있는 구조체

    [Header("기타")]
    public int CoinCount = 0;
    bool isDamage=false; //데미지체크

    private int playerHP = 100;
    [Header("애니메이션")]
    public float animationSpeed = 1f;
    public string currentAnimation = "Idle";
    public string nextAnimation = "Run";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float mag = new Vector2(moveDirection.x, moveDirection.y).magnitude;
        animator.SetFloat("Run", mag);
        //--------------------------------------------------------------------------------------//
        PlayerMovement();
        transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        //--------------------------------------------------------------------------------------//
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //--------------------------------------------------------------------------------------//
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRound, groundMask);
        if (isGrounded)
        {
            isjump = true;
        }
        //--------------------------------------------------------------------------------------//
        animator.speed = animationSpeed; //애니메이션이 재생되는 스피드를 조정
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //0번째 레이어에 현재 애니메이션 상태 정보를 반환합니다.

        if (stateInfo.IsName(currentAnimation) && stateInfo.normalizedTime >= 1.0f)
        {
            animator.Play(nextAnimation);
            currentAnimation = nextAnimation;

            //Debug.Log("애니메이션이 끝나고 " + nextAnimation + "애니메이션으로 전환됩니다.");
        }

        if (stateInfo.IsName(currentAnimation))
        {
            Debug.Log("현재 재생 중인 애니메이션 : " + currentAnimation);

            Debug.Log("현재 애니메이션 진행 상태 : " + stateInfo.normalizedTime);
        }
    }
    public void PlayerMovement()
    {
        if (moveDirection.x > 0) //눌렀을때 x좌표가 0보다 크다면
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // 로테이션의 y값을 변경
            //transform.localScale = new Vector3(5, 5, 0); // 스케일 값을 변경
        }
        else if (moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
        }
    }
    void OnMovement(InputValue value) //On + "Actions name" 함수이름으로 지정
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //눌러진값이 있을때
        {
            moveDirection = new Vector3(input.x, input.y, 0); // 무브디렉션에 눌렀을때의 x,y좌표를 보내준다.
        }
    }

    void OnJump()
    {
        if(isGrounded)
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, jumpHeight);
            audioSource.PlayOneShot(audioClip[1]);
            animator.SetTrigger("isJump");
        }
        else if(isjump)
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, jumpHeight);
            audioSource.PlayOneShot(audioClip[2]);
            isjump = false;
            animator.SetTrigger("isJump");
        }
            //rig2D.AddForceY(jumpHeight, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)//트리거가 충돌시 한번 실행
    {
        //if(collision.gameObject.name.Contains("Trap"))
        //{
        //    PlayerDamage(20);
        //}
        if (collision.gameObject.name.StartsWith("w"))//오브젝트가 하나만 존재할 때
        {
            isjump = true;
        }
        if (collision.gameObject.CompareTag("DieZone") || collision.gameObject.CompareTag("Spike"))
        {
            OnDead();
        }
        if (collision.gameObject.tag == "Monster")
        {
            
            if(isDamage == false)
            {
                playerHP -= 10;
                animator.SetTrigger("Damage");
                isDamage = true;
                StartCoroutine(DamageCount());
                if(playerHP <=0)
                {
                    OnDead();
                }
            }  
        }
        if(collision.gameObject.name == "Check")
        {
            isCheck = true;
        }
        Debug.Log("OnTriggerEnter2D" + collision.gameObject.name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == groundMask)
        {
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundMask)
        {
            transform.SetParent(null);
        }
    }
    public virtual void OnDead()
    {
        if(isCheck)
        {
            //Respon(secondObj);
        }
        else
        {
            //Respon(defaultObj);
        }
        audioSource.PlayOneShot(audioClip[0]);      
        //gameOver.SetActive(true);
        Invoke("RectiveateGameOver", 0.3f);
    }
    public void Respon(GameObject obj)
    {
        transform.position = obj.transform.position;
    }
    void RectiveateGameOver()
    {
        //gameOver.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = collsionColor;
            //Gizmos.DrawWireCube(transform.position + (Vector3)boxCollider.offset, boxCollider.size * (Vector2)transform.localScale);
            Gizmos.DrawWireCube(transform.position + (Vector3)boxCollider.offset, boxCollider.size * boxClliderSize);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRound);
        }
    }
   
    IEnumerator DamageCount()//무적시간
    {
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
    }
    public void CoinUp(int coin)
    {
        CoinCount += coin;
        audioSource.PlayOneShot(audioClip[3]);
        Debug.Log("Player함수 CoinUp 실행");
    }
    public void PlayerDamage(int damage)
    {
        if(!isDamage)
        {
            playerHP -= damage;
            animator.SetTrigger("isDamage");
            audioSource.PlayOneShot(audioClip[0]);
            isDamage = true;
            StartCoroutine(DamageCount());
            Debug.Log(playerHP);
        }  
    }
}
