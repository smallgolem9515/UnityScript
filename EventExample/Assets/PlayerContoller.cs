using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerContoller : MonoBehaviour
{
    //UnityEvent : 유니티에서 제공해주는 콜백을 진행할 수 있는 도구
    //c#의 이벤트처럼 기능들을 등록해두고 필요할 때마다 호출합니다.
    //특징 : 이벤트를 인스펙터에 노출시켜서 개발자가 작업을 바로 할당할 수 있게
    //       도와줍니다.
    //장점 : C#의 델리게이트, 이벤트 개념이 익숙하지 않은 분들에게 효과적입니다.
    //단점 : 무조건 인스펙터에 노출되는 방식
    public UnityEvent onDEAD;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dead();
    }
    private void Dead()
    {
        onDEAD.Invoke(); //이벤트 실행
        Destroy(gameObject);
    }
}
