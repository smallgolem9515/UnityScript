using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//����Ƽ�� Ȱ���ؼ� ���� ������ ȹ�� ����� 
//�����غ����� �սô�.

public class CreateItem : MonoBehaviour
{
   
    public GameObject item; //���� ������
    public List<GameObject> item_list;
    public Text Stone_text;
    public List<string> Stones = new List<string>();
    
    
    
    
    
    
    public Transform create_position; //������ ��ġ
    public Vector3 position; //���ͷ� ����
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
        // 0 ~ �������� ���� -1����

        //��ǥ ����
        position.x = Random.Range(0, 5f);
        position.y = Random.Range(0, 5f);

        Instantiate(random_item, position, Quaternion.identity);
    }
    public void OnCreateButtonDown()
    {
        Instantiate(item); //����
    }
    public void OnCreateButtonDown2()
    {
        Instantiate(item, position, Quaternion.identity);
        //Quaternion.identity : ȸ�� X
    }
}

