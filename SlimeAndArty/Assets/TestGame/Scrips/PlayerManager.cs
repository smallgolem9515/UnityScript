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
    [Header("�÷��̾� ������")]
    Vector3 moveDirection; //OnMove�� ���� ������ ����3
    float speed = 3.0f; //�����̴� �ӵ�
    public int CoinCount = 0;
    bool isDamage = false; //������üũ
    private int playerHP = 100;
    Vector3 defaltPosition;
    public bool isdie=false;
    [Header("����")]
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
    [Header("�ִϸ��̼�")]
    public float animationSpeed = 1f;
    [Header("-----FadeInFadeOut-----")]
    public Image blackPanel; //������ ȭ��
    public float fadeDuration = 1f; //���̵���, ���̵�ƿ� �ӵ�
    public string nextScenName; //������
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
        animator.speed = animationSpeed; //�ִϸ��̼��� ����Ǵ� ���ǵ带 ����
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //0��° ���̾ ���� �ִϸ��̼� ���� ������ ��ȯ�մϴ�.
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
        if (collision.gameObject.name.StartsWith("w"))//������Ʈ�� �ϳ��� ������ ��
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
    //���̵��� ȿ���� ó���ϰ� ���� �ε��ϴ� �ڷ�ƾ
    IEnumerator FadeInAndLoadScene()
    {
        if (!isFading)
        {
            isFading = true;
            yield return StartCoroutine(FadeImage(0, 1, fadeDuration)); //�г� ���̵���
            //���İ��� 0�̸� ���� 1�̸� ������
            SceneManager.LoadScene(nextScenName); //�� �ε�

        }
        else if (isFading)
        {
            yield return StartCoroutine(FadeImage(1,0,fadeDuration)); //�� �ε� �� ���̵� �ƿ�
            //�����ϻ� ����� ��������ʴ´�.
            isFading=false; //���̵� ���� �� ���� �ʱ�ȭ
        }

    }
    //Image�� Alpha ���� �����Ͽ� ���̵� ȿ���� ó���ϴ� �ڷ�ƾ
    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        Color panelColor = blackPanel.color; //�г� ���� ��������
        //���̵� ȿ�� ���� Alpha ���� ���������� ��ȭ
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //�����Ӵ� �ð��� �����ȴ�.
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            //�������� ���������� 1�� ���������.

            panelColor.a = newAlpha;
            blackPanel.color = panelColor;//�г� ���� ������Ʈ

            yield return null; //�����Ӹ��� ����
        }
        //duration�� �Ѿ�� Ż��
        //���������� ���� �� ����
        panelColor.a = endAlpha;
        blackPanel.color = panelColor;
    }
}
