using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Test : MonoBehaviour
//{

//    public float maxDistance = 1000f;
//    RaycastHit hit;

//    public Vector3 boxSize = new Vector3(1, 1, 1); // BoxCast�� ũ��
//    public Vector3 direction = Vector3.forward;   // BoxCast�� �̵��� ����
//    public float distance = 5.0f;                 // BoxCast�� �̵� �Ÿ�
//    public Vector3 boxCenter = Vector3.zero;      // BoxCast�� ���� ��ġ(���� ����)
//    public LayerMask layerMask;                   // �浹�� ������ ���̾�

//    private void OnDrawGizmos()
//    {
//        Gizmos�� ������ ����
//        Gizmos.color = Color.red;

//        BoxCast�� ���� ��ġ(���� ����)
//        Vector3 origin = transform.position + boxCenter;

//        BoxCast�� �� ��ġ ���
//        Vector3 endPos = origin + direction.normalized * distance;

//        BoxCast�� �ڽ� ũ���� ���� ����ϹǷ�, �׸� ���� ������ ����
//        Vector3 halfBoxSize = boxSize / 2;

//        BoxCast�� ���� �ڽ� �׸���
//        Gizmos.DrawWireCube(origin, boxSize);

//        BoxCast�� �� �ڽ� �׸���
//        Gizmos.DrawWireCube(endPos, boxSize);

//        BoxCast�� �������� ������ �����ϴ� �� �׸���
//        Gizmos.DrawLine(origin, endPos);
//    }

//    BoxCast�� ������ �����Ͽ� �浹 ����(����׿�)
//    void Update()
//    {
//        RaycastHit hit;
//        if (Physics.BoxCast(transform.position + boxCenter, boxSize / 2, direction, out hit, Quaternion.identity, distance, layerMask))
//        {
//            �浹�� ������Ʈ ���� ���
//            Debug.Log("Hit: " + hit.collider.name);
//        }
//    }
//    private void Update()
//    {
//        Ray ray = new Ray(transform.position, transform.forward);
//        //                    ����                 ����
//        //RaycastHit[] hits = Physics.RaycastAll(ray,maxDistance);
//        bool result = Physics.BoxCast(transform.position, new Vector3(10, 10, 10), transform.forward);
//        if (result)
//        {
//            Debug.Log("�浹�� ������Ʈ: " + hit.collider.name);
//        }
//        if (hits.Length > 0)
//        {
//            foreach (RaycastHit hit in hits)
//            {
//                Debug.Log("�浹�� ������Ʈ: " + hit.collider.name);
//                Debug.DrawLine(transform.position, transform.forward * maxDistance, Color.red);
//            }
//        }
//        else
//        {
//            Debug.DrawLine(transform.position, transform.forward * maxDistance, Color.green);
//        }
//    }
//}
