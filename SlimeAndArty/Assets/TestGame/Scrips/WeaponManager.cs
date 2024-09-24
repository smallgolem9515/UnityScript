using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    private Camera mainCamera; //ī�޶�
    private Vector3 mousepos; //���콺�� ��ġ ����

    [Header("Bullet")]
    public GameObject bulletObj; //�Ѿ�
    public Transform bulletPos; //�Ѿ��� ������ ��ġ

    public bool isFire = false; //���� ��� ����
    //bool���� �̸��� is,be,on�����ִµ� is�� ��õ
    private float timer = 0; // �ð����庯��
    private float delayTime = 0.3f;
    float rayDistance = 1000f;
    public int poolSize = 100; //������Ʈ Ǯ�� ������
    private List<GameObject> bulletPool; //������Ʈ Ǯ ����Ʈ
    RaycastHit2D raycastHit;
    AudioSource bulletSound; //�߻���
    
    void Start()
    {
        bulletSound = GetComponent<AudioSource>(); 
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
        mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousepos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : �Լ��� ��ǥ��鿡�� ���������κ��� �� �������� ������ ���ϴ� �Լ�

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
        if(isFire && PlayerManager.instance.isdie == false)
        {
            // ���콺 ��ġ�� �����ͼ� ���� ��ǥ�� ��ȯ
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���� ������Ʈ ��ġ���� ���콺 ���������� ���� ���
            Vector2 rayDirection = (mousePosition - (Vector2)bulletPos.position).normalized;

            // RaycastHit2D�� ���̰� �浹�� ������Ʈ�� ������ ����ϴ�.
            RaycastHit2D hit = Physics2D.Raycast(bulletPos.position, rayDirection, rayDistance);

            // ���̰� ���𰡿� �浹�ߴٸ�
            if (hit.collider)
            {
                Debug.DrawRay(bulletPos.position, rayDirection * rayDistance, Color.black);
                // �浹�� ������Ʈ�� �̸� ���
                Debug.Log("�浹�� ������Ʈ: " + hit.collider.name);
            }

            // ����׿� ���� �ð�ȭ (Scene â���� ���� ��θ� Ȯ��)
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
