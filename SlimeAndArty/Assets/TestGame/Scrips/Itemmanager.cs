using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemmanager : MonoBehaviour
{
    float timer = 0;
    public float timerLimit = 3.0f;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            timer += Time.deltaTime;
            if (timer > timerLimit)
            {
                spriteRenderer.enabled = true;
                boxCollider.enabled = true;
                isRunning = true;
                timer = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        isRunning = false;
    }
}
