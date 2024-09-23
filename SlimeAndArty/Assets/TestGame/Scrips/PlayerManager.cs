using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : PlayerActive
{
    public static PlayerManager instance {  get; private set; }
    [Header("�ܺ�����")]
    //public GameObject defaultObj; //���������
    //public GameObject secondObj; //üũ����Ʈ�� ������
    //public GameObject gameOver; // ���ӿ���ȭ��
    public List<AudioClip> audioClip; //�Ҹ�������

    [Header("������Ʈ")]
    Rigidbody2D rig2D;
    Camera mainCam;
    protected AudioSource audioSource;
    BoxCollider2D boxCollider;
    Animator animator;

    [Header("������")]
    public float jumpHeight; //��������
    Vector3 moveDirection; //OnMove�� ���� ������ ����3
    float speed = 3.0f; //�����̴� �ӵ�
    public bool isjump; //��������üũ

    [Header("�����")]
    public Color collsionColor = Color.red; //������
    public Vector2 boxClliderSize; //�ݶ��̴��� ũ��

    [Header("�׶���üũ")]
    public bool isCheck = false; //üũ����Ʈ ����
    private bool isGrounded = false; //��üũ
    public Transform groundCheck; //üũ�� ��ġ
    public float groundRound = 0.1f; //üũ����ġ���� �浹������ ���� ������
    public LayerMask groundMask; //���̾��ũ�� ���̾��� ������ ����ִ� ����ü

    [Header("��Ÿ")]
    public int CoinCount = 0;
    bool isDamage=false; //������üũ

    private int playerHP = 100;
    [Header("�ִϸ��̼�")]
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
        animator.speed = animationSpeed; //�ִϸ��̼��� ����Ǵ� ���ǵ带 ����
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //0��° ���̾ ���� �ִϸ��̼� ���� ������ ��ȯ�մϴ�.

        if (stateInfo.IsName(currentAnimation) && stateInfo.normalizedTime >= 1.0f)
        {
            animator.Play(nextAnimation);
            currentAnimation = nextAnimation;

            //Debug.Log("�ִϸ��̼��� ������ " + nextAnimation + "�ִϸ��̼����� ��ȯ�˴ϴ�.");
        }

        if (stateInfo.IsName(currentAnimation))
        {
            Debug.Log("���� ��� ���� �ִϸ��̼� : " + currentAnimation);

            Debug.Log("���� �ִϸ��̼� ���� ���� : " + stateInfo.normalizedTime);
        }
    }
    public void PlayerMovement()
    {
        if (moveDirection.x > 0) //�������� x��ǥ�� 0���� ũ�ٸ�
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // �����̼��� y���� ����
            //transform.localScale = new Vector3(5, 5, 0); // ������ ���� ����
        }
        else if (moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
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

    private void OnTriggerEnter2D(Collider2D collision)//Ʈ���Ű� �浹�� �ѹ� ����
    {
        //if(collision.gameObject.name.Contains("Trap"))
        //{
        //    PlayerDamage(20);
        //}
        if (collision.gameObject.name.StartsWith("w"))//������Ʈ�� �ϳ��� ������ ��
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
   
    IEnumerator DamageCount()//�����ð�
    {
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
    }
    public void CoinUp(int coin)
    {
        CoinCount += coin;
        audioSource.PlayOneShot(audioClip[3]);
        Debug.Log("Player�Լ� CoinUp ����");
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
