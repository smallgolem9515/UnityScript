using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class second : MonoBehaviour
{
    //�ؽ�Ʈ ������Ʈ�� ID�� LEVEL�� EXP�� ���� ������ ��ũ��Ʈ �۾��� ����
    //�־��ְ� �ͽ��ϴ�.
    public Text id_text;
    public Text level_text;
    public Text exp_text;

    public string id; 
    [Range(1,99)]public int level;
    //����Ƽ �ν����Ϳ��� 1���� 99 ������ ������ ������ �� �ֽ��ϴ�.(�� ��)
    [Range(0.0f, 100.0f)] public float exp;
    private void Update()
    {
        id_text.text = id;
        level_text.text = level.ToString();
        exp_text.text = exp.ToString();
        
    }
}
