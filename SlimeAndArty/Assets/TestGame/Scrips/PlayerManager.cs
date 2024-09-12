using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    Vector3 moveDirection; //벡터3 정의
    AudioSource audioSource;
    Rigidbody2D rig2D;
    float speed = 3.0f;
    public GameObject defaultObj;
    public GameObject gameOver;
    public BoxCollider2D boxCollider;
    public Color collsionColor = Color.red;
    public Vector2 boxClliderSize;
    public float jumpHeight;
    public bool isjump;
    Camera mainCam;
    [Header("오디오 클립")]
    public List<AudioClip> audioClip;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float groundRound = 0.1f;
    public LayerMask groundMask; //레이어마스크는 레이어의 정보를 담고있는 구조체

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        if (moveDirection.x > 0) //눌렀을때 x좌표가 0보다 크다면
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // 로테이션의 y값을 변경
            //transform.localScale = new Vector3(5, 5, 0); // 스케일 값을 변경
        }
        else if(moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
        }
        transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        //Mathf.Abs는 넣은 float값의 절대값을 return한다.
        //Vector3(x,y,z)에 계산식을 넣을시 각 좌표에 다 넣어진다.
        //Debug.Log(moveDirection.x);
        //각 키를 누를시 약0.7에서 1까지의 값이 넣어진다. 거기에 3.0*프레임을 넣은값이 곱해져서 속도가 나온다.
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRound, groundMask);
        if (isGrounded)
        {
            isjump = true;
        }
        
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
    void OnMovement(InputValue value) //On + "Actions name" 함수이름으로 지정
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //눌러진값이 있을때
        {
            moveDirection = new Vector3(input.x, input.y, 0); // 무브디렉션에 눌렀을때의 x,y좌표를 보내준다.
        }
    }
    public void OnDead()
    {
        audioSource.PlayOneShot(audioClip[0]);
        Respon();
        gameOver.SetActive(true);
        Invoke("RectiveateGameOver", 0.3f);
    }
    void OnJump()
    {
        if(isGrounded)
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, jumpHeight);
            audioSource.PlayOneShot(audioClip[1]);
        }
        else if(isjump)
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, jumpHeight);
            audioSource.PlayOneShot(audioClip[2]);
            isjump = false;
        }
            //rig2D.AddForceY(jumpHeight, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)//트리거가 충돌시 한번 실행
    {
        
        if (collision.gameObject.name.StartsWith("w"))//오브젝트가 하나만 존재할 때
        {
            isjump = true;
        }
        if(collision.gameObject.CompareTag("DieZone"))
        {
            OnDead();
        }
        if (collision.gameObject.tag == "Monster")
        {
            OnDead();
        }
        //if (collision.gameObject.tag == "Monster"
        //    || collision.gameObject.tag == "Spike")
        //{
        //diyingAudioSource.Play();
        //Respon();
        //gameOver.SetActive(true);
        //Invoke("RectiveateGameOver", 0.3f);
        //}
        Debug.Log("OnTriggerEnter2D" + collision.gameObject.name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            //jumpCount = 2;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //jumpCount--;
    }
    public void Respon()
    {
        transform.position = defaultObj.transform.position;
    }
    void RectiveateGameOver()
    {
        gameOver.SetActive(false);
    }
}
