using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    //XXXEnter
    //XXXExit
    //XXXStay

    //����(�浹ü)�� �浹�� �߻����� �� ȣ���� �޼ҵ�
    //https://docs.unity3d.com/kr/2019.4/Manual/ExecutionOrder.html ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�� �߻��߽��ϴ�.");
        Destroy(collision.gameObject);
        //��ü �ı�
    }
    //�浹�� ������ �� ȣ���� �޼ҵ�
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("�浹�� �������ϴ�.");
    //}
    ////�浹 ���� ���� ȣ���� �޼ҵ�
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("�浹�� �������Դϴ�.");
    //}
    
}
