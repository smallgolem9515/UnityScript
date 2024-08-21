using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Tooltip("�۾��� ���������� Item ���� ���� �����͸� �־��ּ���.")]
    public Item item; //�����ۿ� ���� �۾� �߰�

    [SerializeField]
    private Text AppleText;
    [SerializeField]
    private Text ItemcountText;
    //private�� �����͸� �ν����Ϳ����� ���� �� �ֵ��� ó�����ݴϴ�.
    //����Ʈ�� ����ȭ(Serialize)
    //������Ʈ�� �����͸� �ٸ� ȯ�濡���� ����� �� �ְ� ���ִ� �۾�

    //������ ��� ���
    //1. public�� �̿��� �������� ���
    //2. ����Ƽ ��θ� �����ؼ� �ڵ带 ���� ���
    //�̹� �ڵ忡�� 1���� �۾�

    private void Start()
    {
        SetText();
        SetItemName();
    }
    public void SetItemName()
    {
        AppleText.text = item.name;
    }
    public void Plus()
    {
        item.count++;
        SetText();
    }
    public void Minus()
    {
        item.count--;
        SetText();
    }
    public  void SetText()
    {
        ItemcountText.text = $"���� ��� ���� ���� : {item.count : #,##0}��";
    }
}
