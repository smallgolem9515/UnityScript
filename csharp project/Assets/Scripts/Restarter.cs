using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public Transform start_position;
    //����Ƽ���� ��ǥ, ȸ��, ������ ���� ǥ���ϴ� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = start_position.transform.position;
        //�浹ü�� ��ġ�� �����س��� ���� ��ġ�� �����մϴ�.
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //�ӷ��� 0���� �����մϴ�.
    }
}
