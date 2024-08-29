using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pla : MonoBehaviour
{
    public UnityEvent ONdead;
    public float speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dead();
    }

    private void Dead()
    {
        ONdead.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, y, 0) *speed * Time.deltaTime;
        transform.position += move;
    }

}
