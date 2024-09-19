using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Camera maincamera;
    Vector3 mouseFos;
    Rigidbody2D rid2D;
    WeaponManager weaponManager;
    float bulletSpeed = 30f;
    void Start()
    {
        rid2D = GetComponent<Rigidbody2D>();
        weaponManager = FindObjectOfType<WeaponManager>();

        maincamera = Camera.main;
        
        mouseFos = maincamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mouseFos - transform.position;
        Vector3 rotation = transform.position - mouseFos;
        rid2D.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        float rotationZ = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + 90);
        //+90 : 총알이 직선모양으로 나가도록 보정(총알의 모양에 따라 주는 값을 조정한다.)
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player"
            && collision.tag != "Spike")
        {
            weaponManager.ReturnBulletToPool(gameObject);
            //Destroy(gameObject);
        }
        //gameObject.SetActive(false); //오브젝트 활성화 및 비활성화
        
    }
}
