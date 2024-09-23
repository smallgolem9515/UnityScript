using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(CoinRespon());
            PlayerManager.instance.CoinUp(1);
            //����++(���ӿ� ����)(�ϴ� Player)
            gameObject.SetActive(false);//��Ȱ��ȭ
            //�Ҹ����(Player���)
        }
    }
    
    IEnumerator CoinRespon()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(true);
    }
}
