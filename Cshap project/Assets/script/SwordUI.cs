using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordUI : MonoBehaviour
{
    public Sword sword;
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text SwordText;

    public void SetMoneyText()
    {
        //MoneyText.text = Sword.money;
    }
    
}
