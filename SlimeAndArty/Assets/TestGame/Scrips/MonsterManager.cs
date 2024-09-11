using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    Rigidbody2D m_Rigidbody2D;
    public float speed = 2.0f;
    public float maxDistance = 3.0f;
    public int direction = 1;
    Vector3 startPosi;
    public int moveList;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        startPosi = transform.position;
    }

    private void Update()
    {
        if (moveList == 1)
        {
            WidthMove();
        }
        else if (moveList == 2) 
        {
            LengthMove();
        }
        
    }
    void WidthMove()
    {
        if (transform.position.x > startPosi.x + maxDistance)
        {
            direction = -1;
        }
        else if (transform.position.x < startPosi.x - maxDistance)
        {
            direction = 1;
        }
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
    }
    void LengthMove()
    {
        if (transform.position.y > startPosi.y + maxDistance)
        {
            direction = -1;
        }
        else if (transform.position.y < startPosi.y - maxDistance)
        {
            direction = 1;
        }

        //m_Rigidbody2D.AddForce(new Vector2(direction,direction)*speed * Time.deltaTime, ForceMode2D.Impulse);
        transform.position += new Vector3(0, direction, 0) * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            m_AudioSource.Play();
            Destroy(gameObject, 0.5f);
        }  
    } 
}
