using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    //XXXEnter
    //XXXExit
    //XXXStay

    //상대방(충돌체)과 충돌이 발생했을 때 호출할 메소드
    //https://docs.unity3d.com/kr/2019.4/Manual/ExecutionOrder.html 참고
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌이 발생했습니다.");
        Destroy(collision.gameObject);
        //물체 파괴
    }
    //충돌이 끝났을 때 호출할 메소드
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("충돌이 끝났습니다.");
    //}
    ////충돌 진행 동안 호출할 메소드
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("충돌이 진행중입니다.");
    //}
    
}
