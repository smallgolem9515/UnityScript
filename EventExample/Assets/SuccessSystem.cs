using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessSystem : MonoBehaviour
{
    public Text text;
    public void MissonClear()
    {
        text.text = "미션 클리어!";
    }
    public void MissonClear(string title)
    {
        text.text = $"{title} 미션을 클리어했습니다!";
    }
}
