using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Test : MonoBehaviour
//{

//    public float maxDistance = 1000f;
//    RaycastHit hit;

//    public Vector3 boxSize = new Vector3(1, 1, 1); // BoxCast의 크기
//    public Vector3 direction = Vector3.forward;   // BoxCast가 이동할 방향
//    public float distance = 5.0f;                 // BoxCast의 이동 거리
//    public Vector3 boxCenter = Vector3.zero;      // BoxCast의 시작 위치(로컬 기준)
//    public LayerMask layerMask;                   // 충돌을 감지할 레이어

//    private void OnDrawGizmos()
//    {
//        Gizmos의 색상을 설정
//        Gizmos.color = Color.red;

//        BoxCast의 시작 위치(월드 기준)
//        Vector3 origin = transform.position + boxCenter;

//        BoxCast의 끝 위치 계산
//        Vector3 endPos = origin + direction.normalized * distance;

//        BoxCast는 박스 크기의 반을 사용하므로, 그릴 때도 반으로 조정
//        Vector3 halfBoxSize = boxSize / 2;

//        BoxCast의 시작 박스 그리기
//        Gizmos.DrawWireCube(origin, boxSize);

//        BoxCast의 끝 박스 그리기
//        Gizmos.DrawWireCube(endPos, boxSize);

//        BoxCast의 시작점과 끝점을 연결하는 선 그리기
//        Gizmos.DrawLine(origin, endPos);
//    }

//    BoxCast를 실제로 실행하여 충돌 감지(디버그용)
//    void Update()
//    {
//        RaycastHit hit;
//        if (Physics.BoxCast(transform.position + boxCenter, boxSize / 2, direction, out hit, Quaternion.identity, distance, layerMask))
//        {
//            충돌한 오브젝트 정보 출력
//            Debug.Log("Hit: " + hit.collider.name);
//        }
//    }
//    private void Update()
//    {
//        Ray ray = new Ray(transform.position, transform.forward);
//        //                    시작                 방향
//        //RaycastHit[] hits = Physics.RaycastAll(ray,maxDistance);
//        bool result = Physics.BoxCast(transform.position, new Vector3(10, 10, 10), transform.forward);
//        if (result)
//        {
//            Debug.Log("충돌한 오브젝트: " + hit.collider.name);
//        }
//        if (hits.Length > 0)
//        {
//            foreach (RaycastHit hit in hits)
//            {
//                Debug.Log("충돌한 오브젝트: " + hit.collider.name);
//                Debug.DrawLine(transform.position, transform.forward * maxDistance, Color.red);
//            }
//        }
//        else
//        {
//            Debug.DrawLine(transform.position, transform.forward * maxDistance, Color.green);
//        }
//    }
//}
