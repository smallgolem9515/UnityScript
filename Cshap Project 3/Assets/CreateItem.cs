using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//유니티를 활용해서 랜덤 아이템 획득 기능을 
//연출해보도록 합시다.

public class CreateItem : MonoBehaviour
{
   
    public GameObject item; //게임 아이템
    public List<GameObject> item_list;
    public Text Stone_text;
    public List<string> Stones = new List<string>();
    
    
    
    
    
    
    public Transform create_position; //생성할 위치
    public Vector3 position; //벡터로 설정
    public List<Sprite> item_sprites;
    public Image random_image;

    public void OnRandomImage()
    {
        random_image.sprite = item_sprites[Random.Range(0,item_sprites.Count)];
    }
    public void OnRandomStone()
    {
        
    }

    public void ONCreateRandom()
    {
        var random_item = item_list[Random.Range(0, item_list.Count)];
        // 0 ~ 아이템의 개수 -1까지

        //좌표 랜덤
        position.x = Random.Range(0, 5f);
        position.y = Random.Range(0, 5f);

        Instantiate(random_item, position, Quaternion.identity);
    }
    public void OnCreateButtonDown()
    {
        Instantiate(item); //생성
    }
    public void OnCreateButtonDown2()
    {
        Instantiate(item, position, Quaternion.identity);
        //Quaternion.identity : 회전 X
    }
}

