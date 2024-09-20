using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : PlayerActive
{
    [Header("�ܺ�����")]
    public GameObject defaultObj; //���������
    public GameObject secondObj; //üũ����Ʈ�� ������
    public GameObject gameOver; // ���ӿ���ȭ��
    public List<AudioClip> audioClip; //�Ҹ�������
    [Header("������Ʈ")]
    Rigidbody2D rig2D;
    Camera mainCam;
    protected AudioSource audioSource;
    BoxCollider2D boxCollider;
    Animator animator;
    [Header("������")]
    Vector3 moveDirection; //OnMove�� ���� ������ ����3
    float speed = 3.0f; //�����̴� �ӵ�
    public float jumpHeight; //��������
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
    bool isDamage; //������üũ
    float timer = 0; //�ð����

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
        //float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vartical");

        float mag = new Vector2(moveDirection.x, moveDirection.y).magnitude;
        animator.SetFloat("Run", mag);
        Debug.Log(mag);
        if (isDamage)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                isDamage = false;
                timer = 0;
            }
        }
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

        //mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
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
                animator.SetTrigger("Damage");
                isDamage = true;
                OnDead();
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
            Respon(secondObj);
        }
        else
        {
            Respon(defaultObj);
        }
        audioSource.PlayOneShot(audioClip[0]);      
        gameOver.SetActive(true);
        Invoke("RectiveateGameOver", 0.3f);
    }
    public void Respon(GameObject obj)
    {
        transform.position = obj.transform.position;
    }
    void RectiveateGameOver()
    {
        gameOver.SetActive(false);
    }

}
