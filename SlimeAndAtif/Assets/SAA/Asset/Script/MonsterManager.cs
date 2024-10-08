using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public enum Monster
    {
        Vomfy,
        SkullWolf
    }
    public enum State
    {
        Idle,
        Moving,
        Attack,
        Damage,
        Die
    }
    public float hp;
    bool isJelly = false;
    Transform playerPosition;
    public float attackRange = 1.0f;
    public float attackCoolDown = 2.0f;
    private float nextAttackTime = 0f;
    public bool isDamage = false;
    public float moveSpeed = 2.0f;
    Animator animator;
    Sprite Sprite;
    State currentState;
    Monster currentMonster;
    void Start()
    {
        Sprite = GetComponent<Sprite>();
        animator = GetComponent<Animator>();
        currentState = State.Moving;
        playerPosition = PlayerManagerSlime.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position,playerPosition.position);
        
        Debug.Log(distanceToTarget);
        if (hp > 0 && currentState == State.Moving)
        {
            if (transform.position.x < playerPosition.position.x)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
            if (distanceToTarget > attackRange)
            {
                Vector3 direction = (playerPosition.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                animator.SetBool("isMoving",true);
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    StartCoroutine(Attack());
                    nextAttackTime = Time.time + attackCoolDown;
                }
            }
        }
        else
        {
            animator.SetBool("isMoving",false);
        }
    }
    IEnumerator Attack()
    {
        if(currentMonster == Monster.Vomfy)
        {
            SoundManager.instance.PlaySFX("VomfyAttack");
        }
        else if(currentMonster == Monster.SkullWolf)
        {
            SoundManager.instance.PlaySFX("WolfAttack");
        }
        currentState = State.Attack;
        PlayerManagerSlime.instance.PlayerDamage(1);
        Debug.Log("플레이어 공격");
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(0.33f);
        currentState = State.Moving;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Particlemanager.instance.PlayParticle("GreenEffect1", collision.gameObject.transform.position, gameObject.layer + 1);
            Damage();
        }
        if (collision.gameObject.tag == "Player")
        {
            if (isJelly)
            {
                gameObject.SetActive(false);
            }
        }
    }
    void Damage()
    {
        if(currentMonster == Monster.SkullWolf)
        {
            SoundManager.instance.PlaySFX("WolfDamage");
        }
        currentState = State.Damage;
        hp -= PlayerManagerSlime.instance.playerAttackPower;
        isDamage = true;

        Debug.Log(hp);
        if (hp <= 0)
        {
            currentState = State.Die;
            animator.SetTrigger("isDie");
            gameObject.tag = "Jelly";
            gameObject.layer = 6;
            isJelly = true;
            if (currentMonster == Monster.SkullWolf)
            {
                SoundManager.instance.PlaySFX("WolfDie");
            }
            else
            {
                SoundManager.instance.PlaySFX("MonsterDie");
            }
        }
        else
        {
            StartCoroutine(DamageCount());
        }
    }
    IEnumerator DamageCount()//경직
    {
        animator.SetTrigger("isDamage");
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
        currentState = State.Moving;
    }
}
