using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Rigidbody2D rid2D;
    PlayerManager playerManager;
    void Start()
    {
        rid2D = GetComponent<Rigidbody2D>();
        playerManager = FindObjectOfType<PlayerManager>();
        
        if (playerManager.dir == 0)
        {
            //rid2D.AddForce(new Vector2(5.0f,0),ForceMode2D.Force); //ÈûÀ» ÀÚµ¿Â÷ ¿¢¼¿À» ¹â´Â ´À³¦À¸·Î ÈûÀ» Á¡Á¡ °¡ÇÑ´Ù.
            rid2D.AddForce(new Vector2(30.0f, 0), ForceMode2D.Impulse); //ÈûÀ» ºıÁÖ´Â ´À³¦
        }
        else
        {
            rid2D.AddForce(new Vector2(-30.0f, 0), ForceMode2D.Impulse); //ÈûÀ» ºıÁÖ´Â ´À³¦
        }

    }
    
}
