using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text state_text;

    //텍스트에 대한 기능
    public void OnDeadPlayer()
    {
        state_text.color = Color.red;
        state_text.text = "죽었습니다.";
    }

    public void OnStartPlayer()
    {
        state_text.color = Color.yellow;
        state_text.text = "플레이어가 들어왔습니다!";
    }

}
