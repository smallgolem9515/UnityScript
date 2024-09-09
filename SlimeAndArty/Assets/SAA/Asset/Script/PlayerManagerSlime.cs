using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSlime : MonoBehaviour
{
    float playerSpeed = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputSystem();
    }
    void PlayerInputSystem()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if ((Input.GetKey(KeyCode.S)))
        {
            transform.Translate(new Vector3(0, -playerSpeed * Time.deltaTime, 0));
        }
        if (((Input.GetKey(KeyCode.W))))
        {
            transform.Translate(new Vector3(0, playerSpeed * Time.deltaTime, 0));
        }
    }
}
