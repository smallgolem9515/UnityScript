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
    private Camera mainCamera; //ī�޶�
    private Vector3 mousePos; //���콺�� ��ġ ����

    [Header("Bullet")]
    public GameObject bulletObj; //�Ѿ�
    public Transform[] bulletPos; //�Ѿ��� ������ ��ġ

    public bool isFire = true; //���� ��� ����
    //bool���� �̸��� is,be,on�����ִµ� is�� ��õ
    private float delayTime = 0.1f;

    [Header("ObjectPool")]
    public int poolSize = 100; //������Ʈ Ǯ�� ������
    private List<GameObject> bulletPool; //������Ʈ Ǯ ����Ʈ

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
    public int pelletCount = 10; //���� źȯ ����
    public float spreaAngle = 30; //����źȯ�� ������ ����
    public float knockbackForce = 10f; //�浹�� ������Ʈ�� �о�� ��

    private int pistolBulletCount = 100;
    private int shougunBulletCount = 100;

    [Header("CameraShake Settings")]
    public float shakeDuration = 0.1f; //��鸲 ���� �ð�
    public float shakeMagnitude = 0.2f; //��鸲 ����
    private Vector3 originalPos; //ī�޶� ���� ��ġ

    void Start()
    { 
        mainCamera = Camera.main; // ����ī�޶� �־��ֱ�
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
        //���콺 ��ġ ������Ʈ �� ���� ��ǥ ��ȯ
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //���콺 ��������
        Vector3 rotation = mousePos - transform.position;

        rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : �Լ��� ��ǥ��鿡�� ���������κ��� �� �������� ������ ���ϴ� �Լ�

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
    //Ǯ���� ��Ȱ��ȭ�� �Ѿ��� �������� �Լ�
    GameObject GetBulletFromPool()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if(!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        //Ǯ�� ����� �� �ִ� �Ѿ��� ������ Null ��ȯ
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
        // ���� ������Ʈ ��ġ���� ���콺 ���������� ���� ���
        rayDirection = (mousePos - bulletPos[0].position).normalized;

        // RaycastHit2D�� ���̰� �浹�� ������Ʈ�� ������ ����ϴ�.
        RaycastHit2D hit = Physics2D.Raycast(bulletPos[0].position, rayDirection, rayDistance);

        // ���̰� ���𰡿� �浹�ߴٸ�
        if (hit.collider)
        {
            Debug.DrawRay(bulletPos[0].position, rayDirection * rayDistance, Color.black);
            // �浹�� ������Ʈ�� �̸� ���
            Debug.Log("�浹�� ������Ʈ: " + hit.collider.name);
        }
        // ����׿� ���� �ð�ȭ (Scene â���� ���� ��θ� Ȯ��)
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
            //������ ������ ���
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
            Debug.Log("���� ī�޶� ã�� �� �����ϴ�. MainCamera Tag�� Ȯ�����ּ���.");
            yield break;
        }
        CameraManager.instance.isShake = true;
        float elapsed = 0.0f; //��� �ð� �ʱ�ȭ
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
