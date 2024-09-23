using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    private Camera mainCamera; //카메라
    private Vector3 mousepos; //마우스의 위치 저장

    [Header("Bullet")]
    public GameObject bulletObj; //총알
    public Transform bulletPos; //총알이 생성될 위치

    public bool isFire = false; //총을 쏘는 여부
    //bool형의 이름은 is,be,on등이있는데 is를 추천
    private float timer = 0; // 시간저장변수
    private float delayTime = 0.3f;

    public int poolSize = 100; //오브젝트 풀링 사이즈
    private List<GameObject> bulletPool; //오브젝트 풀 리스트

    AudioSource bulletSound; //발사음
    
    void Start()
    {
        bulletSound = GetComponent<AudioSource>(); 
        mainCamera = Camera.main; // 메인카메라 넣어주기
        bulletPool = new List<GameObject>();
        for(int i = 0; i <  poolSize;i++)
        {
            GameObject bullet = Instantiate(bulletObj);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    void Update()
    {
        mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousepos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : 함수는 좌표평면에서 수평축으로부터 한 점까지의 각도를 구하는 함수

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if(!isFire)
        {
            timer += Time.deltaTime;
            if(timer > delayTime)
            {
                isFire = true;
                timer = 0;
            }
        }        
    }
    //풀에서 비활성화된 총알을 가져오는 함수
    GameObject GetBulletFromPool()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if(!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        //풀에 사용할 수 있는 총알이 없으면 Null 반환
        return null;
    }
    void OnClick()
    {
        if(isFire)
        {
            GameObject bullet = GetBulletFromPool();
            if(bullet != null)
            {
                bullet.transform.position = bulletPos.position;
                bullet.transform.rotation = bulletPos.rotation;
                bullet.SetActive(true);
            }
            isFire = false;
            bulletSound.Play();
        }       
    }
    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> ();
        rb.velocity = Vector2.zero;
    }
}
