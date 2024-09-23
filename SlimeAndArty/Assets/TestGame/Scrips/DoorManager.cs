using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public KeyManager keyManager;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyManager.door1)
        {
            boxCollider2D.isTrigger = true;
        }
    }
}
