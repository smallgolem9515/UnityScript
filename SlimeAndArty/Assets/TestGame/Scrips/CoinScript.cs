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
            //코인++(게임에 따라)(일단 Player)
            gameObject.SetActive(false);//비활성화
            //소리재생(Player재생)
        }
    }
    
    IEnumerator CoinRespon()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(true);
    }
}
