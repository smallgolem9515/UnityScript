using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private Camera mainCamera; //카메라
    private Vector3 mousePos; //마우스의 위치 저장

    [Header("Bullet")]
    public GameObject bulletObj; //총알
    public Transform[] bulletPos; //총알이 생성될 위치

    public bool isFire = true; //총을 쏘는 여부
    //bool형의 이름은 is,be,on등이있는데 is를 추천
    private float delayTime = 0.1f;

    [Header("ObjectPool")]
    public int poolSize = 100; //오브젝트 풀링 사이즈
    private List<GameObject> bulletPool; //오브젝트 풀 리스트

    [Header("RayCast")]
    public LayerMask targetLayer;
    float rayDistance = 100f;
    RaycastHit2D raycastHit;
    private float rotationZ;
    private Vector2 rayDirection;

    [Header("Weapon Settings")]
    public bool isGetPistol = false;
    public bool isGetShouGun = false;
    public LayerMask weaponMask;
    GameObject equipObj;

    [Header("Shotgun Settings")]
    public int pelletCount = 10; //샷건 탄환 갯수
    public float spreaAngle = 30; //샷건탄환이 퍼지는 각도
    public float knockbackForce = 10f; //충돌한 오브젝트를 밀어내는 힘

    private int pistolBulletCount = 100;
    private int shougunBulletCount = 100;

    [Header("CameraShake Settings")]
    public float shakeDuration = 0.1f; //흔들림 지속 시간
    public float shakeMagnitude = 0.2f; //흔들림 강도
    private Vector3 originalPos; //카메라 원래 위치

    void Start()
    { 
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
        //마우스 위치 업데이트 및 월드 좌표 반환
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //마우스 방향으로
        Vector3 rotation = mousePos - transform.position;

        rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : 함수는 좌표평면에서 수평축으로부터 한 점까지의 각도를 구하는 함수

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
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
        StartCoroutine(DelayTime(delayTime));
        if (isFire && PlayerManager.instance.isdie == false)
        {
            if (isGetPistol && pistolBulletCount > 0)
            {
                StartCoroutine(Shake(shakeDuration,shakeMagnitude));
                pistolBulletCount--;
                FirePistol();
                delayTime = 0.2f;
                isFire = false;
                return;
            }
            else if(isGetShouGun && shougunBulletCount > 0)
            {
                shakeMagnitude = 0.3f;
                StartCoroutine(Shake(shakeDuration, shakeMagnitude));
                shougunBulletCount--;
                FireShotGun();
                delayTime = 1f;
                isFire = false;
                return;
            }
            isFire = false;
            Soundsmanager.Instance.PlaySFX("BlankHand");
        }       
    }
    void FirePistol()
    {
        // 현재 오브젝트 위치에서 마우스 방향으로의 벡터 계산
        rayDirection = (mousePos - bulletPos[0].position).normalized;

        // RaycastHit2D는 레이가 충돌한 오브젝트의 정보를 담습니다.
        RaycastHit2D hit = Physics2D.Raycast(bulletPos[0].position, rayDirection, rayDistance);

        // 레이가 무언가에 충돌했다면
        if (hit.collider)
        {
            Debug.DrawRay(bulletPos[0].position, rayDirection * rayDistance, Color.black);
            // 충돌한 오브젝트의 이름 출력
            Debug.Log("충돌한 오브젝트: " + hit.collider.name);
        }
        // 디버그용 레이 시각화 (Scene 창에서 레이 경로를 확인)
        Debug.DrawRay(bulletPos[0].position, rayDirection * rayDistance, Color.red,0.2f);

        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = bulletPos[0].position;
            bullet.transform.rotation = bulletPos[0].rotation;
            bullet.SetActive(true);
        }
        Soundsmanager.Instance.PlaySFX("Pistol");
    }
    void FireShotGun()
    {
        Soundsmanager.Instance.PlaySFX("Shotgun");
        for (int i = 0; i < pelletCount; i++)
        {
            rayDirection = (mousePos - bulletPos[1].position).normalized;
            //퍼지는 각도를 계산
            float randomAngle = Random.Range(-spreaAngle / 2f, spreaAngle / 2f);
            Vector2 spreadDirection = Quaternion.Euler(0, 0, randomAngle) * rayDirection;

            RaycastHit2D hit = Physics2D.Raycast(bulletPos[1].position, spreadDirection, rayDistance, targetLayer);
            
            Debug.DrawRay(bulletPos[1].position, spreadDirection * rayDistance, Color.red,0.5f);
            if (hit.collider != null)
            {
                Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    Vector2 knockbackDirection = (hit.collider.transform.position - bulletPos[1].position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> ();
        rb.velocity = Vector2.zero;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        if (Camera.main != null)
        {
            originalPos = new Vector3(CameraManager.instance.shakePosition.x, CameraManager.instance.shakePosition.y, -10);
        }
        else
        {
            Debug.Log("메인 카메라를 찾을 수 없습니다. MainCamera Tag를 확인해주세요.");
            yield break;
        }
        CameraManager.instance.isShake = true;
        float elapsed = 0.0f; //경과 시간 초기화
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            mainCamera.transform.position = new Vector3(originalPos.x, originalPos.y + y, -10);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = originalPos;
        CameraManager.instance.isShake = false;
    }
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        isFire = true;
    }
    void OnEKey()
    {
        RaycastHit2D ehit = Physics2D.Raycast(transform.position, transform.right, 2, weaponMask);
        Debug.DrawRay(transform.position, transform.right * 2, Color.red, 0.2f);
        Debug.Log("Ekey");
        if (ehit)
        {
            if(ehit.collider.name == "Pistol")
            {
                if(isGetShouGun)
                {
                    equipObj.transform.SetParent(null);
                    isGetShouGun = false;
                }
                ehit.collider.transform.SetParent(transform);
                ehit.collider.transform.localPosition = Vector3.zero;
                isGetPistol = true;
                equipObj = ehit.collider.gameObject;
            }
            else if(ehit.collider.name == "Shotgun")
            {
                if (isGetPistol)
                {
                    equipObj.transform.SetParent(null);
                    isGetPistol = false;
                }
                ehit.collider.transform.SetParent(transform);
                ehit.collider.transform.localPosition = Vector3.zero;
                isGetShouGun = true;
                equipObj = ehit.collider.gameObject;
            }
            else if(ehit.collider.tag == "PistolBullet")
            {
                int bullet = Random.Range(3, 10);
                pistolBulletCount += bullet;
                if(pistolBulletCount > 99)
                {
                    pistolBulletCount = 99;
                }
            }
            else if (ehit.collider.tag == "ShougunBullet")
            {
                int bullet = Random.Range(3, 10);
                shougunBulletCount += bullet;
                if(shougunBulletCount > 99)
                {
                    shougunBulletCount = 99;
                }
            }
        }
    }
}
