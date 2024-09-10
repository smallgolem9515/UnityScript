using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float bulSpeed = 10.0f;
    Rigidbody2D rig2D;
    PlayerManagerSlime slime;
    
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        slime = FindObjectOfType<PlayerManagerSlime>();

        if(slime.transform.lossyScale.x > 0)
        {
            rig2D.AddForce(new Vector2(bulSpeed, 0), ForceMode2D.Impulse);
        }
        else if(slime.transform.lossyScale.x < 0)
        {
            rig2D.AddForce(new Vector2(-bulSpeed, 0), ForceMode2D.Impulse);
        }
    }

}
