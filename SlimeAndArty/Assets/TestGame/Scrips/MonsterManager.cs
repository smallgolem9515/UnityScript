using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public float monsterHP = 30;
    public Transform target;
    public float attackRange = 1.0f;
    public float attackCoolDown = 2.0f;
    private float nextAttackTime = 0f;
    private float trackingRange = 5.0f;
    private float evadeRange = 5.0f;
    public bool isDamage = false;

    public Transform[] patrolPoints; //순찰 경로의 지점들
    public float moveSpeed = 2.0f; //이동속도
    private int currentPoint = 0; //현재 순찰 지점 인덱스
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
       
        if(distanceToTarget >= trackingRange)
        {
            if (patrolPoints.Length > 0)
            {
                Patrol();
            }
        }
        else
        {
            AttackAndTraking();
        }
        if (moveSpeed > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (monsterHP < 2.0f)
        {
            Evade();
        }
        if(WeaponManager.instance.isShotDamage)
        {
            if(!isDamage)
            {
                monsterHP -= 10;
                Debug.Log(monsterHP);
                isDamage = true;
                StartCoroutine(DamageCount());
                animator.SetTrigger("isHurt");
                if (monsterHP <= 0)
                {
                    animator.SetTrigger("isDie");
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); //공격범위
    }
    void Attack()
    {
        Debug.Log("플레이어 공격");
        animator.SetTrigger("isAttack");
        PlayerManager.instance.PlayerDamage(10);
    }
    void Evade()
    {
        Debug.Log("도망중");
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPoint]; //목표 포인트 지정
        Vector3 direction = (targetPoint.position - transform.position).normalized; //방향 지정
        transform.position += direction * moveSpeed * Time.deltaTime; //목표로 이동

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            StartCoroutine(StopMove());
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
    void AttackAndTraking()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCoolDown;
        }
        else if (distanceToTarget > attackRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    IEnumerator StopMove()
    {
        moveSpeed = 0;
        animator.Play("SlimeIdle");
        yield return new WaitForSeconds(3.0f);
        moveSpeed = 2.0f;
    }
    IEnumerator DamageCount()//무적시간
    {
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            monsterHP -= 10;
            if(monsterHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    } 
}
