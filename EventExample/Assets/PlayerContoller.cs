using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerContoller : MonoBehaviour
{
    //UnityEvent : ����Ƽ���� �������ִ� �ݹ��� ������ �� �ִ� ����
    //c#�� �̺�Ʈó�� ��ɵ��� ����صΰ� �ʿ��� ������ ȣ���մϴ�.
    //Ư¡ : �̺�Ʈ�� �ν����Ϳ� ������Ѽ� �����ڰ� �۾��� �ٷ� �Ҵ��� �� �ְ�
    //       �����ݴϴ�.
    //���� : C#�� ��������Ʈ, �̺�Ʈ ������ �ͼ����� ���� �е鿡�� ȿ�����Դϴ�.
    //���� : ������ �ν����Ϳ� ����Ǵ� ���
    public UnityEvent onDEAD;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dead();
    }
    private void Dead()
    {
        onDEAD.Invoke(); //�̺�Ʈ ����
        Destroy(gameObject);
    }
}
