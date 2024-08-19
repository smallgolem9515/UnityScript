using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클래스(class)
//개채에 대한 설계를 위해 만드는 설계 틀

//class 클래스명
//{
//    //필드(field) : 클래스 내부에서 만들어진 변수, 개체의 속성을 표현하는 용도로 사용됩니다.

//    //메소드(merhod) : 클래스 내부에서 만들어진 함수, 개체의 동작, 기능을 표현하는 용도로 사용합니다.

//}

//유니티에서 만드는 순수한 class는
//월드에 배치한 오브젝트에 직접 연결할 수 없습니다.

//유니티에서 클래스에 대한 정보를 인스펙터로부터 확인할 수 있게 하는 기능(Serializable)
[Serializable]
public class player
{
    public int hp; //체력
    public int max_hp; //최대 체력
    public string name;
    public Sprite image; //Sprite는 유니티에서 2D 이미지 파일에 대한 도구
    
    public void Dance()
    {
        Debug.Log($"{name}이 춤을 추고 있습니다.");
    }

    public  void Heal()
    {
        if (hp >= max_hp)
        {
            Debug.Log("체력이 이미 가득 차있습니다.");
            return; //void 함수에서 return만 사용하면 함수의 종료를 의미합니다.
        }
        hp += 10;
        Debug.Log($"체력을 10 회복합니다! 현재 체력 {hp} / {max_hp}");
    }
}
