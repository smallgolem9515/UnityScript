using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public float hp;
    bool isJelly = false;
    public Transform playerPosition;
    public float attackRange = 1.0f;
    public float attackCoolDown = 2.0f;
    private float nextAttackTime = 0f;
    public bool isDamage = false;
    public float moveSpeed = 2.0f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position,playerPosition.position);
        if (hp > 0 || isDamage == false)
        {
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
                    Attack();
                    nextAttackTime += Time.time + attackCoolDown;
                }
            }
        }
        else
        {
            animator.SetBool("isMoving",false);
        }
    }
    void Attack()
    {
        PlayerManagerSlime.instance.PlayerDamage(1);
        Debug.Log("플레이어 공격");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Particlemanager.instance.PlayParticle("GreenEffect1", collision.gameObject.transform.position, gameObject.layer+1);
            hp -= PlayerManagerSlime.instance.playerAttackPower;
            isDamage = true;
            
            Debug.Log(hp);
            if (hp <= 0)
            {
                gameObject.tag = "Jelly";
                gameObject.layer = 6;
                isJelly = true;
            }
            else
            {
                StartCoroutine(DamageCount());
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if (isJelly)
            {
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator DamageCount()//경직
    {
        yield return new WaitForSeconds(0.2f);
        isDamage = false;
    }
}
