using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public enum Mode
{
    A, B, C
}
//�ν����Ϳ��� ���� ���� ������ �����մϴ�. [Flags]
[Flags]
public enum Mode2
{
    None = 0, //�ƹ��͵� ���õ��� �ʾ����� ǥ��
    A = 1 << 0, //1
    B = 1 << 1, //2
    C = 1 << 2, //4
    D = 1 << 3  //8

    //��Ʈ ������
    //��Ʈ ���꿡 ���� �۾��� ������ �� ����ϴ� �������Դϴ�.

    //shift ������ <<, >>�� ��Ʈ�� �̵���Ű�� ������ ������ �ֽ��ϴ�.

    //����) 5 >> 2
    //10���� -> 2������ ���
    //     ��   8421
    //     5     101
    // 5 >> 2    001 | 01
    //             1

}
//����Ƽ���� ����� �� �ִ� �Ϲ����� ��ɵ鿡 ���� Ŭ����

//�ش� ����� �߰��ϸ� ����Ƽ ������ ���� AddComponentMenu�� �׸����� �����˴ϴ�.
[AddComponentMenu("DataBasic/DataBasic1")]
public class DataBasic : MonoBehaviour
{
    //����Ƽ �⺻ ��Ģ
    //1. public���� �ۼ��� �ʵ� ���� ����Ƽ�� �ν��夼��
    //   ������ �˴ϴ�.
    //2. public���� ������ ������ ����Ƽ �ν����Ϳ� �ۼ��� ����
    //   �ֿ켱���մϴ�.
    //3. �迭�̳� ����Ʈ ���� ���� new�� ���� ���
    //   ������ �˴ϴ�.

    public int value;
    [TextArea(1, 5)] string word;
    public char alpha;
    public float value2;
    public bool isdead;

    [ContextMenuItem("�迭 �ʱ�ȭ", "ArrayReset")]
    public int[] array;
    //�ش� �ʵ忡�� ������ ��ư�� ������ ���� â�� �߰�, ������ �־��� �Լ��� �����մϴ�.

    public List<int> int_list;
    public Mode mode;
    public Mode2 mode2;

    private void Start()
    {
        
    }

    //������Ʈ���� ������ ��ư�� ������ ��� ������ ���
    [ContextMenu("������ ����")]
    void DataSetting()
    {
        value = 5;
        word = "��ο�";
        alpha = 'a';
        value2 = 1.34f;
        isdead = true;
        array = new int[5];
        int_list = new List<int>();
        mode = Mode.A;
        mode2 = Mode2.A | Mode2.B;
    }

    void ArrayReset()
    {
        array = new int[5];
    }
}
