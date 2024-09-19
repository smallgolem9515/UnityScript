using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int blocknumber;
    float timer;
    PlayerManager playermanager;


    public List<GameObject> blocks;
    public float scrollSpeed = 2f;
    float blockScailx;
    float blockPosiy;
    
    private void Start()
    {
        playermanager = FindObjectOfType<PlayerManager>();
        timer = 0;
        blockScailx = blocks[0].transform.localScale.x;
        blockPosiy = blocks[0].transform.position.y;
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.position = new Vector3(i * blockScailx+20, blockPosiy+Random.Range(0,5), blocks[i].transform.position.z);
        }
    }
    private void Update()
    {
        if(blocknumber == 4)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].transform.Translate(Vector3.left*scrollSpeed*Time.deltaTime);
                if (blocks[i].transform.position.x <= blockScailx)
                {
                    blocks[i].transform.position =
                    new Vector3(blocks[i].transform.position.x * blockScailx+20, blockPosiy * Random.Range(0,5), blocks[i].transform.position.z);
                }
            }
        }
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
