using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text state_text;

    //�ؽ�Ʈ�� ���� ���
    public void OnDeadPlayer()
    {
        state_text.color = Color.red;
        state_text.text = "�׾����ϴ�.";
    }

    public void OnStartPlayer()
    {
        state_text.color = Color.yellow;
        state_text.text = "�÷��̾ ���Խ��ϴ�!";
    }

}
