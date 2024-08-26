using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public int Quest_number; //플레이어가 진행하는 퀘스트 넘버
    public QuestManager QuestManager; //퀘스트 매니저 연결

    //public bool flip = false;

    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //움직인 방향키대로 좌표 값 설정
        Vector3 move = new Vector3 (h, v, 0) * speed * Time.deltaTime;

        if (h < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (h > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //해당 컴포넌트를 가진 오브젝트의 위치를 그 수치만큼 더하기
        transform.position += move;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            QuestManager.QuestRun(); //퀘스트 매니저의 퀘스트 런 실행
        }
    }
}
