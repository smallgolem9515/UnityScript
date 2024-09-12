using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int blocknumber;
    float timer;
    PlayerManager playermanager;
    private void Start()
    {
        playermanager = FindObjectOfType<PlayerManager>();
        timer = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(blocknumber == 0)
        {
            playermanager.OnDead();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (blocknumber == 1)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer > 2f)
            {
                playermanager.OnDead();
                timer = 0;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (blocknumber == 2)
        {
            playermanager.OnDead();

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (blocknumber == 3)
        {
            playermanager.OnDead();

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (blocknumber == 4)
        {
            timer += Time.deltaTime;
            if (timer < 2f)
            {
                playermanager.OnDead();
                timer = 0;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (blocknumber == 5)
        {
            playermanager.OnDead();

        }
    }
}
