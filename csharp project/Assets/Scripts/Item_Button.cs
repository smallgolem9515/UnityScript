using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Button : MonoBehaviour
{
    public GameObject shop_button;
    public PlayerControlrer playerControlrer;
    Button button1;

    private void Start()
    {
        button1 = shop_button.GetComponent<Button>();

        button1.onClick.AddListener(Button_Click);
    }
    public void Button_Click()
    {
        playerControlrer.Max_speed += 5;
    }
}
