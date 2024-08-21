using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ؽ�Ʈ�� �ϳ��ϳ� �ܾ�� ��µǴ� ����� �����߽��ϴ�.
public class TypingText : MonoBehaviour
{
    [Header("Ÿ������ �ؽ�Ʈ")] public Text text;
    [Header("������")] public Item item;
    string message = ""; //����� �޼����� ���� ����

    void Start()
    {
        message = item.description; //�������� ������ �޼����� ����
        StartCoroutine(Typing(message)); //�ڷ�ƾ�� �����մϴ�.
    }
    //�ڷ�ƾ ���� ����
    //StartCoroutine(IEnumerator ������ �Լ� �̸�(�Ű�������)); �ڷ�ƾ �۵�
    //StopCoroutine(IEnumerator ������ �Լ�  �̸�(�Ű�������)); �ڷ�ƾ �۾� ����
    //StopAllCoroutine(); //��� �ڷ�ƾ �۾��� ����

    //����Ƽ�� �ڷ�ƾ(Coroutine) : �۾��� �����ϴ�, ���� ����, �ð� �� Ż�� ��Ű��
    //�ٽ� �۾����� ���ƿ��� ó���� �� �� �ְ� ���ִ� ����
    IEnumerator Typing(string message)
    {
        text.text = null; //���� ����

        //���޹��� �޼����� ���̸�ŭ �ݺ��� �����մϴ�.
        for (int i = 0; i < message.Length; i++)
        {
            text.text += message[i];
            yield return new WaitForSeconds(0.05f);
        }

        //0.5�� ���� �۾� Ż��
        //yield�� ȣ���ڿ��� �����͸� �ϳ��� �����ϴ� �۾��� ���� �� ����ϴ� Ű�����Դϴ�.
        //yield return : �÷����� �����͸� �ϳ��� ����
        //yield break : ������ �����ϰ� ������ ���������� �뵵
    }
    //���� ����(����)
    //�޼����� ���̸�ŭ �ܾ� �ϳ��ϳ� �߰�(�ؽ�Ʈ ������Ʈ��)
    //0.5�ʸ��� �۾��� �ٽ� ����
}
