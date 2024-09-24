using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    Animator animator;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyManager.instance.door1)
        {
            animator.enabled = true;
            boxCollider2D.isTrigger = true;
        }
    }
}
