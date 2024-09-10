using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManagerSlime : MonoBehaviour
{
    float playerSpeed = 5.0f;
    public GameObject bulletObj;
    Vector3 vec3;
    public Transform bulletPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vec3.x > 0)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }
        else if (vec3.x < 0)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }
        transform.Translate(vec3 * playerSpeed*Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        Vector2 vec2 = value.Get<Vector2>();
        if (value != null)
        {
            vec3 = new Vector3(vec2.x, vec2.y, 0);
        }        
    }
    void OnFire()
    {
        Instantiate(bulletObj, bulletPos.transform.position,Quaternion.identity);
    }
}
