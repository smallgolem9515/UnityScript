using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public Transform start_position;
    //유니티에서 좌표, 회전, 스케일 등을 표현하는 데이터

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = start_position.transform.position;
        //충돌체의 위치를 설정해놓은 시작 위치로 설정합니다.
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //속력을 0으로 설정합니다.
    }
}
