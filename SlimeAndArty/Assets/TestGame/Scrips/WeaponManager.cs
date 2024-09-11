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

    AudioSource bulletSound; //발사음
    

    void Start()
    {
        bulletSound = GetComponent<AudioSource>(); 
        mainCamera = Camera.main; // 메인카메라 넣어주기
        
    }

    void Update()
    {
        mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousepos - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //Atan() : 함수는 좌표평면에서 수평축으로부터 한 점까지의 각도를 구하는 함수
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
