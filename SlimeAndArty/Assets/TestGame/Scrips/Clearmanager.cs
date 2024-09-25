using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearmanager : MonoBehaviour
{
    public GameObject clearPoint;
    AudioSource clearSource;
    // Start is called before the first frame update
    void Start()
    {
        clearSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            clearPoint.SetActive(true);
            clearSource.Play();
            Invoke("ReStart", 2f);
            //playerManager.Respon(playerManager.defaultObj);
            PlayerManager.instance.isCheck = false;
        }
    }
    void ReStart()
    {
        clearPoint.SetActive(false);

    }
}
