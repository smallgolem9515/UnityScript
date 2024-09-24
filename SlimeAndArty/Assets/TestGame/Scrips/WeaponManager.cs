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
    float rayDistance = 1000f;
    public int poolSize = 100; //오브젝트 풀링 사이즈
    private List<GameObject> bulletPool; //오브젝트 풀 리스트
    RaycastHit2D raycastHit;
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
        Debug.DrawLine(mousepos, raycastHit.point, Color.red);
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
        if(isFire && PlayerManager.instance.isdie == false)
        {
            // 마우스 위치를 가져와서 월드 좌표로 변환
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 현재 오브젝트 위치에서 마우스 방향으로의 벡터 계산
            Vector2 rayDirection = (mousePosition - (Vector2)bulletPos.position).normalized;

            // RaycastHit2D는 레이가 충돌한 오브젝트의 정보를 담습니다.
            RaycastHit2D hit = Physics2D.Raycast(bulletPos.position, rayDirection, rayDistance);

            // 레이가 무언가에 충돌했다면
            if (hit.collider)
            {
                Debug.DrawRay(bulletPos.position, rayDirection * rayDistance, Color.black);
                // 충돌한 오브젝트의 이름 출력
                Debug.Log("충돌한 오브젝트: " + hit.collider.name);
            }

            // 디버그용 레이 시각화 (Scene 창에서 레이 경로를 확인)
            Debug.DrawRay(bulletPos.position, rayDirection * rayDistance, Color.red);
            //raycastHit = Physics2D.Raycast(new Vector2(bulletPos.position.x, bulletPos.position.y), mousepos, 1000);
            //Debug.Log(raycastHit.collider.name);
            //if(raycastHit.collider.tag != "Player")
            //{
            //    raycastHit.collider.gameObject.SetActive(false);   
            //}
            //GameObject bullet = GetBulletFromPool();
            //if(bullet != null)
            //{
            //    bullet.transform.position = bulletPos.position;
            //    bullet.transform.rotation = bulletPos.rotation;
            //    bullet.SetActive(true);
            //}
            //isFire = false;
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
