using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŭ����(class)
//��ä�� ���� ���踦 ���� ����� ���� Ʋ

//class Ŭ������
//{
//    //�ʵ�(field) : Ŭ���� ���ο��� ������� ����, ��ü�� �Ӽ��� ǥ���ϴ� �뵵�� ���˴ϴ�.

//    //�޼ҵ�(merhod) : Ŭ���� ���ο��� ������� �Լ�, ��ü�� ����, ����� ǥ���ϴ� �뵵�� ����մϴ�.

//}

//����Ƽ���� ����� ������ class��
//���忡 ��ġ�� ������Ʈ�� ���� ������ �� �����ϴ�.

//����Ƽ���� Ŭ������ ���� ������ �ν����ͷκ��� Ȯ���� �� �ְ� �ϴ� ���(Serializable)
[Serializable]
public class player
{
    public int hp; //ü��
    public int max_hp; //�ִ� ü��
    public string name;
    public Sprite image; //Sprite�� ����Ƽ���� 2D �̹��� ���Ͽ� ���� ����
    
    public void Dance()
    {
        Debug.Log($"{name}�� ���� �߰� �ֽ��ϴ�.");
    }

    public  void Heal()
    {
        if (hp >= max_hp)
        {
            Debug.Log("ü���� �̹� ���� ���ֽ��ϴ�.");
            return; //void �Լ����� return�� ����ϸ� �Լ��� ���Ḧ �ǹ��մϴ�.
        }
        hp += 10;
        Debug.Log($"ü���� 10 ȸ���մϴ�! ���� ü�� {hp} / {max_hp}");
    }
}
