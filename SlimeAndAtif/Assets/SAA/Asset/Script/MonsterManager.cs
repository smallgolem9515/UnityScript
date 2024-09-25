using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int hp;
    bool isJelly;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp--;
            if (hp <= 0)
            {
                gameObject.layer = 6;
                isJelly = true;
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.layer == 6)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
