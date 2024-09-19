using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject[] backGrounds; //�������� ��׶����
    public float backGroundSpeed = 2.0f; //��׶��尡 �������� �ӵ�
    private float backGroundWidth; //��׶����� ������
    
    void Start()
    {
        backGroundWidth = backGrounds[0].transform.localScale.x+3; //ù��°�� ������ ����

        for(int i = 0;i< backGrounds.Length; i++)
        {
            backGrounds[i].transform.position = 
            new Vector3(i * backGroundWidth, backGrounds[i].transform.position.y,backGrounds[i].transform.position.z);
            // �� ��׶����� x�� �����Ͽ� ���缭 ���������� ����
        }
    }


    void Update()
    {
        for(int i = 0; i< backGrounds.Length; i++)
        {
            backGrounds[i].transform.Translate(Vector3.left*backGroundSpeed*Time.deltaTime);
            //�������� �귯����.

            if (backGrounds[i].transform.position.x <= backGroundWidth-10 ) //���� ��׶����� x�� �����Ϻ��� �۰ų� ������
            {
                Vector3 rightPosition = GetRightBackGroundPosition(); 

                backGrounds[i].transform.position =
                new Vector3(rightPosition.x + backGroundWidth, backGrounds[i].transform.position.y, backGrounds[i].transform.position.z);
                //��ŸƮ ��ġ�� ����
            }
            
        }
    }
    private Vector3 GetRightBackGroundPosition()
    {
        Vector3 rightPosition = backGrounds[0].transform.position;//ù��° ��ġ����
        rightPosition.x += 5;
        for (int i = 1; i < backGrounds.Length; i++)
        {
            if (backGrounds[i].transform.position.x > rightPosition.x)// ���� ��ġ�� ù��°���� �տ� ������
            {
                rightPosition = backGrounds[i].transform.position; //����Ʈ�������� ������ġ�� ����
            }
        }
        return rightPosition; //����Ʈ�������� ����
    }
}
