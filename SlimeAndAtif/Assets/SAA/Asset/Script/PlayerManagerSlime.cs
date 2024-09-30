using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManagerSlime : MonoBehaviour
{
    public static PlayerManagerSlime instance;

    [Header("플레이어의 수치")]
    public int hp;
    public int maxHP = 6;
    public int limitHP = 12;
    public int count;
    public int maxCount = 6;
    public float playerSpeed = 5.0f;
    public float delayTime = 0.3f;
    public bool isClear = false;
    public bool isDamage = false;
    public bool isFire = true;

    [Header("외부 참조")]
    public GameObject bulletObj;
    public Transform bulletPos;
    Vector2 move4Posi;
    Vector3 moveInput;
    [Header("컴포넌트")]
    Animator animator;
    Rigidbody2D rig2D;
    public float shakeDuration = 0.1f; //흔들림 지속 시간
    public float shakeMagnitude = 0.2f; //흔들림 강도
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
    void Start()
    {
        hp = maxHP;
        count = maxCount;
        animator = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        move4Posi = transform.position;
        PlayerFlip();
        float mag = new Vector2(moveInput.x, moveInput.y).magnitude;
        animator.SetFloat("Run", mag);
        transform.Translate(moveInput * playerSpeed*Time.deltaTime);

        rig2D.velocity = new Vector2(0, 0); 
    }
    void PlayerFlip()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(7, 7, 7);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-7, 7, 7);
        }
    }
    void BulletPosition(float x, float y)
    {
        move4Posi += new Vector2(x*1, y*1);
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
                BulletPosition(moveInput.x, moveInput.y);
                Instantiate(bulletObj, move4Posi, Quaternion.identity);
                count--;
                isFire = false;
                StartCoroutine(FireDelay(delayTime));
                animator.SetTrigger("isFireTrigger");
            }
        }  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Monster")
        //{
        //    PlayerDamage(1);
        //}
        if (collision.gameObject.tag == "Jelly")
        {
            hp += 2;
            count += 3;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!isDamage)
        {
            if (collision.gameObject.tag == "Monster")
            {
                PlayerDamage(1);
            }
        }
        
    }
    public void PlayerDamage(int damage)
    {
        if (!isDamage)
        {
            isDamage = true;
            hp -= damage;
            if (hp % 2 == 0)
            {
                StartCoroutine(UIManager.instance.EmptyHeart(hp / 2));
            }
            else
            {
                StartCoroutine(UIManager.instance.HalfHeart(hp / 2));
            }
            StartCoroutine(CameraManager.instance.Shake(shakeDuration, shakeMagnitude));
            StartCoroutine(DamageDelay());
        }
    }
    public IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
    }
    public IEnumerator FireDelay(float time)
    {
        yield return new WaitForSeconds(time);
        isFire = true;
    }
}
