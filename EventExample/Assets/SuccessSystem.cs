using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessSystem : MonoBehaviour
{
    public Text text;
    public void MissonClear()
    {
        text.text = "�̼� Ŭ����!";
    }
    public void MissonClear(string title)
    {
        text.text = $"{title} �̼��� Ŭ�����߽��ϴ�!";
    }
}
