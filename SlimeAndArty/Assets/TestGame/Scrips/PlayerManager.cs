using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    Vector3 moveDirection; //����3 ����
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
    [Header("����� Ŭ��")]
    public List<AudioClip> audioClip;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float groundRound = 0.1f;
    public LayerMask groundMask; //���̾��ũ�� ���̾��� ������ ����ִ� ����ü

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        rig2D = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        if (moveDirection.x > 0) //�������� x��ǥ�� 0���� ũ�ٸ�
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // �����̼��� y���� ����
            //transform.localScale = new Vector3(5, 5, 0); // ������ ���� ����
        }
        else if(moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
        }
        transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        //Mathf.Abs�� ���� float���� ���밪�� return�Ѵ�.
        //Vector3(x,y,z)�� ������ ������ �� ��ǥ�� �� �־�����.
        //Debug.Log(moveDirection.x);
        //�� Ű�� ������ ��0.7���� 1������ ���� �־�����. �ű⿡ 3.0*�������� �������� �������� �ӵ��� ���´�.
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
    void OnMovement(InputValue value) //On + "Actions name" �Լ��̸����� ����
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //���������� ������
        {
            moveDirection = new Vector3(input.x, input.y, 0); // ����𷺼ǿ� ���������� x,y��ǥ�� �����ش�.
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

    private void OnTriggerEnter2D(Collider2D collision)//Ʈ���Ű� �浹�� �ѹ� ����
    {
        
        if (collision.gameObject.name.StartsWith("w"))//������Ʈ�� �ϳ��� ������ ��
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
