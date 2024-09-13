using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float bulSpeed = 10.0f;
    Rigidbody2D rig2D;
    PlayerManagerSlime slime;
    Vector3 mousePoint;
    Camera cam;
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        slime = FindObjectOfType<PlayerManagerSlime>();
        cam = Camera.main;
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diretion = mousePoint - transform.position;
        Vector3 rotation = transform.position - mousePoint;
        rig2D.velocity = new Vector2(diretion.x, diretion.y).normalized * bulSpeed;
        float rotationZ = Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotationZ+90);

        //if(slime.transform.lossyScale.x > 0)
        //{
        //    rig2D.AddForce(new Vector2(bulSpeed, 0), ForceMode2D.Impulse);
        //}
        //else if(slime.transform.lossyScale.x < 0)
        //{
        //    rig2D.AddForce(new Vector2(-bulSpeed, 0), ForceMode2D.Impulse);
        //}
    }
    private void Update()
    {
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

}
