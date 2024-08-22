using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //카메라가 플레이어를 따라가게 하기 위한 코드
    //방법1. 코드가 아닌 플레이어의 자식 오브젝트로 카메라를 연결하는 방법
    //       이 방법은 원치 않은 버그가 발생할 확률이 높음
    //       가장 단순한 방법이지만 가장 위험한 방법
    //방법2. 스크립트를 통해 카메라의 기능을 조작하여 플레이어를 따라가게 합니다.

    public float camera_speed = 5.0f; //1. 카메라 속도에 대한 설정

    public GameObject camera_target; //카메라가 추적할 타겟

    private void Update()
    {
        Vector3 dir = camera_target.transform.position - this.transform.position;
        //타겟의 위치 - 카메라 위치 = 카메라와 타겟 간의 거리

        Vector3 m_Vector = new Vector3(dir.x * camera_speed * Time.deltaTime,
            dir.y * camera_speed * Time.deltaTime, 0.0f);

        //거리를 기준으로 벡터 값을 새로 갱신합니다.
        //Time.DeltaTime은 업데이트에서 작업할 때 사용할 보정 값

        transform.Translate(m_Vector);
        //트랜스폼의 위치를 이동시키는 코드 Translate
    }
}
