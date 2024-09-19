using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject[] backGrounds; //지나가는 백그라운드들
    public float backGroundSpeed = 2.0f; //백그라운드가 지나가는 속도
    private float backGroundWidth; //백그라운드의 스케일
    
    void Start()
    {
        backGroundWidth = backGrounds[0].transform.localScale.x+3; //첫번째의 스케일 저장

        for(int i = 0;i< backGrounds.Length; i++)
        {
            backGrounds[i].transform.position = 
            new Vector3(i * backGroundWidth, backGrounds[i].transform.position.y,backGrounds[i].transform.position.z);
            // 각 백그라운드의 x를 스케일에 맞춰서 순차적으로 변경
        }
    }


    void Update()
    {
        for(int i = 0; i< backGrounds.Length; i++)
        {
            backGrounds[i].transform.Translate(Vector3.left*backGroundSpeed*Time.deltaTime);
            //왼쪽으로 흘러간다.

            if (backGrounds[i].transform.position.x <= backGroundWidth-10 ) //현재 백그라운드의 x가 스케일보다 작거나 같을때
            {
                Vector3 rightPosition = GetRightBackGroundPosition(); 

                backGrounds[i].transform.position =
                new Vector3(rightPosition.x + backGroundWidth, backGrounds[i].transform.position.y, backGrounds[i].transform.position.z);
                //스타트 위치로 복귀
            }
            
        }
    }
    private Vector3 GetRightBackGroundPosition()
    {
        Vector3 rightPosition = backGrounds[0].transform.position;//첫번째 위치저장
        rightPosition.x += 5;
        for (int i = 1; i < backGrounds.Length; i++)
        {
            if (backGrounds[i].transform.position.x > rightPosition.x)// 현재 위치가 첫번째보다 앞에 있을때
            {
                rightPosition = backGrounds[i].transform.position; //라이트포지션을 현재위치로 변경
            }
        }
        return rightPosition; //라이트포지션을 리턴
    }
}
