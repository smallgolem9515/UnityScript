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

    AudioSource bulletSound; //�߻���
    

    void Start()
    {
        bulletSound = GetComponent<AudioSource>(); 
        mainCamera = Camera.main; // ����ī�޶� �־��ֱ�
        
    }

    void Update()
    {
        mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousepos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : �Լ��� ��ǥ��鿡�� ���������κ��� �� �������� ������ ���ϴ� �Լ�
        //if (playerManager.dir == 0)
        //{
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        //}
        //else if (playerManager.dir == 1)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, -rotationZ);
        //}

        if(!isFire)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);

            if(timer > delayTime)
            {
                isFire = true;
                timer = 0;
            }
        }
        
    }
    void OnClick()
    {
        if(isFire)
        {
            isFire = false;
            bulletSound.Play();
            Instantiate(bulletObj, bulletPos.transform.position, Quaternion.identity);
        }
        
    }
}
