using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float bulSpeed = 10.0f;
    Rigidbody2D rig2D;
    Vector3 mousePoint;
    Camera cam;
    BoxCollider2D boxCollider2D;
  
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        cam = Camera.main;
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diretion = mousePoint - transform.position;
        Vector3 rotation = transform.position - mousePoint;
        rig2D.velocity = new Vector2(diretion.x, diretion.y).normalized * bulSpeed;
        //rig2D.velocity = transform.position * bulSpeed;
        float rotationZ = Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotationZ);

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
        if(!PlayerManagerSlime.instance.isFire)
        {
            StartCoroutine(ShotTime());
        }
        if(PlayerManagerSlime.instance.isClear)
        {
            ClearBullet();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        boxCollider2D.isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Bullet")
        {
            if (collision.gameObject.tag == "Monster")
            {
                
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag != "Player")
            {
                gameObject.tag = "Jelly";
            }
        }
        else
        {
            rig2D.velocity = new Vector2(0, 0);
            if (collision.gameObject.tag == "Player")
            {
                PlayerManagerSlime.instance.count += 1;
                Destroy(gameObject);
            }
        }
    }
    public void ClearBullet()
    {        
        Destroy(gameObject);
    }
    IEnumerator ShotTime()
    {
        yield return new WaitForSeconds(1);
        boxCollider2D.isTrigger = false;
    }
}
