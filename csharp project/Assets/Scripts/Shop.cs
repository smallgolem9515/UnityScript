using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����
//�÷��̾ NPC�� �浹���� ��� ������ ����

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public bool ison = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ison == false)
        {
            shop.SetActive(true);
            ison = true;
        }
        else
        {
            shop.SetActive(false);
            ison = false;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    ison = false;
    //}
}
