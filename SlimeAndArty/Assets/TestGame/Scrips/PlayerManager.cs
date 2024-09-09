using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float speed = 3.0f;
    
    

    private void Start()
    {
        
    }
    private void Update()
    {
        PlayerInputSystem();    
    }
    void PlayerInputSystem()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            transform.localEulerAngles = new Vector3(0,0,0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            transform.localEulerAngles = new Vector3(0,180,0);
        }
        if ((Input.GetKey(KeyCode.S)))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (((Input.GetKey(KeyCode.W))))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
