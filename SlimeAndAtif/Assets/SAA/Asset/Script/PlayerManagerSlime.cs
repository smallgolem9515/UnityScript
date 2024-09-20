using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManagerSlime : MonoBehaviour
{
    [Header("플레이어의 수치")]
    public int hp;
    public float playerSpeed = 5.0f;
    public int count = 6;
    public float delayTime = 0.3f;
    [Header("외부 참조")]
    public GameObject bulletObj;
    public Transform bulletPos;
    public bool isFire = true;

    Vector3 moveInput;
    float timer = 0;
    [Header("컴포넌트")]
    Animator animator;
    Rigidbody2D rig2D;
    public static PlayerManagerSlime instance;
    private void Awake()
    {
        if (PlayerManagerSlime.instance == null)
        {
            PlayerManagerSlime.instance = this;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(7, 7, 7);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-7, 7, 7);
        }
        float mag = new Vector2(moveInput.x, moveInput.y).magnitude;
        animator.SetFloat("Run", mag);
        transform.Translate(moveInput * playerSpeed*Time.deltaTime);
        if(!isFire)
        {
            timer += Time.deltaTime;
            if(timer > delayTime)
            {
                isFire = true;
                timer = 0;
            }
        }
        rig2D.velocity = new Vector2(0, 0); 
    }
    void OnMove(InputValue value)
    {
        Vector2 vec2 = value.Get<Vector2>();
        if (value != null)
        {
            moveInput = new Vector3(vec2.x, vec2.y, 0);
        }        
    }
    void OnFire()
    {
        if (isFire)
        {
            if (count > 0)
            {
                Instantiate(bulletObj, bulletPos.transform.position, Quaternion.identity);
                count--;
                isFire = false;
                animator.SetTrigger("isFireTrigger");
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            hp--;
            if(collision.gameObject.layer == 6)
            {
                hp += 2;
                count += 3;
            }
        }

    }
}
