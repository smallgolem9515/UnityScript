using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolmanager : MonoBehaviour
{
    float speed = 0.5f;
    Camera cam;
    PlayerManager playerManager;
    Vector3 defultPosi;
    void Start()
    {
        cam = Camera.main;
        //playerManager = FindObjectOfType<PlayerManager>();
        defultPosi = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //if(playerManager.gameOver.activeSelf)
        //{
        //    if(playerManager.isCheck)
        //    {
        //        transform.position = new Vector3(defultPosi.x,defultPosi.y+2.0f, -10);
        //    }
        //    else
        //    {
        //        transform.position = defultPosi;
        //    }
        //}
        Invoke("Running", 1.0f);
    }
    void Running()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
