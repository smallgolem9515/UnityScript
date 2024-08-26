using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public int Quest_number; //�÷��̾ �����ϴ� ����Ʈ �ѹ�
    public QuestManager QuestManager; //����Ʈ �Ŵ��� ����

    //public bool flip = false;

    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //������ ����Ű��� ��ǥ �� ����
        Vector3 move = new Vector3 (h, v, 0) * speed * Time.deltaTime;

        if (h < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //�ش� ������Ʈ�� ���� ������Ʈ�� ��ġ�� �� ��ġ��ŭ ���ϱ�
        transform.position += move;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            QuestManager.QuestRun(); //����Ʈ �Ŵ����� ����Ʈ �� ����
        }
    }
}
