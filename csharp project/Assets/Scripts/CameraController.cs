using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ī�޶� �÷��̾ ���󰡰� �ϱ� ���� �ڵ�
    //���1. �ڵ尡 �ƴ� �÷��̾��� �ڽ� ������Ʈ�� ī�޶� �����ϴ� ���
    //       �� ����� ��ġ ���� ���װ� �߻��� Ȯ���� ����
    //       ���� �ܼ��� ��������� ���� ������ ���
    //���2. ��ũ��Ʈ�� ���� ī�޶��� ����� �����Ͽ� �÷��̾ ���󰡰� �մϴ�.

    public float camera_speed = 5.0f; //1. ī�޶� �ӵ��� ���� ����

    public GameObject camera_target; //ī�޶� ������ Ÿ��

    private void Update()
    {
        Vector3 dir = camera_target.transform.position - this.transform.position;
        //Ÿ���� ��ġ - ī�޶� ��ġ = ī�޶�� Ÿ�� ���� �Ÿ�

        Vector3 m_Vector = new Vector3(dir.x * camera_speed * Time.deltaTime,
            dir.y * camera_speed * Time.deltaTime, 0.0f);

        //�Ÿ��� �������� ���� ���� ���� �����մϴ�.
        //Time.DeltaTime�� ������Ʈ���� �۾��� �� ����� ���� ��

        transform.Translate(m_Vector);
        //Ʈ�������� ��ġ�� �̵���Ű�� �ڵ� Translate
    }
}
