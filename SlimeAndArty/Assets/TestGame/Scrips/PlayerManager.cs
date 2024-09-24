using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Header("플레이어 데이터")]
    Vector3 moveDirection; //OnMove의 값을 저장할 벡터3
    float speed = 3.0f; //움직이는 속도
    public int CoinCount = 0;
    bool isDamage = false; //데미지체크
    private int playerHP = 100;
    Vector3 defaltPosition;
    public bool isdie=false;
    [Header("점프")]
    public float jumpHeight; //점프높이
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
    [Header("애니메이션")]
    public float animationSpeed = 1f;
    [Header("-----FadeInFadeOut-----")]
    public Image blackPanel; //검은색 화면
    public float fadeDuration = 1f; //페이드인, 페이드아웃 속도
    public string nextScenName; //다음씬
    static bool isFading = false;

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
        defaltPosition = transform.position;
        Debug.Log(isFading);
        if(isFading)
        {
           StartCoroutine(FadeInAndLoadScene());
        }
    }
    private void Update()
    {
        float mag = new Vector2(moveDirection.x, moveDirection.y).magnitude;
        animator.SetFloat("Run", mag);
        //--------------------------------------------------------------------------------------//
        if(!isdie)
        {
            PlayerMovement();
            transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        }
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
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //0번째 레이어에 현재 애니메이션 상태 정보를 반환합니다.
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
        if (collision.gameObject.name.StartsWith("w"))//오브젝트가 하나만 존재할 때
        {
            isjump = true;
        }
        if (collision.gameObject.CompareTag("DieZone") || collision.gameObject.CompareTag("Spike"))
        {
            OnDead();
        }
        if(!isDamage && collision.tag != "Bullet")
        {
            if(collision.tag == "Monster")
            {
                PlayerDamage(10);
            }

        }
        if(collision.gameObject.name == "Check")
        {
            isCheck = true;
        }
        if(collision.tag == "Door" && isFading == false)
        {
            audioSource.PlayOneShot(audioClip[1]);
            StartCoroutine(FadeInAndLoadScene());
        }
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
    public void OnDead()
    {  
        //gameOver.SetActive(true);
        Invoke("RectiveateGameOver", 0.3f);
        isdie = true;
    }
    public void Respon(Vector3 vec3)
    {
        transform.position = vec3;
        playerHP = 100;
    }
    void RectiveateGameOver()
    {
        //gameOver.SetActive(false);
        Respon(defaltPosition);
        isdie = false;
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
            if(playerHP <= 0)
            {
                OnDead();
            }
        }  
    }
    public void PlayerFootSound()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(groundCheck.position, groundRound, groundMask);
        Debug.Log(collider2D.tag);
        if(collider2D.CompareTag("Snow"))
        {
            audioSource.PlayOneShot(audioClip[5]);
        }
        //else if(collider2D.CompareTag(""))
        //{
        //    audioSource.PlayOneShot(audioClip[6]);
        //}
        else
        {
            audioSource.PlayOneShot(audioClip[4]);
        }
    }
    //페이드인 효과를 처리하고 씬을 로드하는 코루틴
    IEnumerator FadeInAndLoadScene()
    {
        if (!isFading)
        {
            isFading = true;
            yield return StartCoroutine(FadeImage(0, 1, fadeDuration)); //패널 페이드인
            //알파값이 0이면 투명 1이면 검은색
            SceneManager.LoadScene(nextScenName); //씬 로드

        }
        else if (isFading)
        {
            yield return StartCoroutine(FadeImage(1,0,fadeDuration)); //씬 로드 후 페이드 아웃
            //예시일뿐 현재는 실행되지않는다.
            isFading=false; //페이드 종료 후 상태 초기화
        }

    }
    //Image의 Alpha 값을 조절하여 페이드 효과를 처리하는 코루틴
    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        Color panelColor = blackPanel.color; //패널 색상 가져오기
        //페이드 효과 동안 Alpha 값을 점진적으로 변화
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //프레임당 시간이 축적된다.
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            //프레임이 지나갈수록 1에 가까워진다.

            panelColor.a = newAlpha;
            blackPanel.color = panelColor;//패널 색상 업데이트

            yield return null; //프레임마다 갱신
        }
        //duration을 넘어가서 탈출
        //최종적으로 알파 값 설정
        panelColor.a = endAlpha;
        blackPanel.color = panelColor;
    }
}
