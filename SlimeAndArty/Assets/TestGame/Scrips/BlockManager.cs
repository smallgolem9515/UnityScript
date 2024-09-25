using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int blocknumber;

    public int blocksSize = 10;
    public GameObject blockObj;
    public List<GameObject> blocks;
    public float scrollSpeed = 2f;
    float blockScailx;
    float blockPosiy;
    
    private void Start()
    {      
        for(int i = 0; i < blocksSize;i++)
        {
            GameObject obj = Instantiate(blockObj);
            obj.SetActive(false);
            blocks[i] = obj;
        }
        blockScailx = blocks[0].transform.localScale.x;
        blockPosiy = blocks[0].transform.position.y;
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].transform.position = new Vector3(0,0, 0);
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
                    new Vector3(blocks[i].transform.position.x * blockScailx+20, blockPosiy + Random.Range(0,5), blocks[i].transform.position.z);
                }
            }
        }
    }
    void ObjectByFool()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (!blocks[i].activeInHierarchy)
            {
                blocks[i].transform.position = new Vector3(0, 0, 0);
            }
        }
    }
    
}
