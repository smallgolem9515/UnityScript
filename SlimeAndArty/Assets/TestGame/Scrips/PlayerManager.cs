using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    float speed = 3.0f;
    Vector3 moveDirection; //����3 ����
    Vector3 gunMovePos;
    AudioSource audioSource;
    public GameObject bulletObj;
    public Transform buleetPos;
    public int dir = 0;
    public GameObject gun;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (moveDirection.x > 0) //�������� x��ǥ�� 0���� ũ�ٸ�
        {
            dir = 0;
            //transform.localEulerAngles = new Vector3(0, 0, 0); // �����̼��� y���� ����
            transform.localScale = new Vector3(5, 5, 0); // ������ ���� ����
        }
        else if(moveDirection.x < 0)
        {
            dir = 1;
            //transform.localEulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(-5, 5, 0);
        }
        transform.Translate(new Vector3(moveDirection.x,moveDirection.y,0)*Time.deltaTime*speed);
        //Mathf.Abs�� ���� float���� ���밪�� return�Ѵ�.
        //Vector3(x,y,z)�� ������ ������ �� ��ǥ�� �� �־�����.
        //Debug.Log(moveDirection.x);
        //�� Ű�� ������ ��0.7���� 1������ ���� �־�����. �ű⿡ 3.0*�������� �������� �������� �ӵ��� ���´�.

        Debug.Log($"{gunMovePos.x}{gunMovePos.y}");
    }


    void OnMovement(InputValue value) //On + "Actions name" �Լ��̸����� ����
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //���������� ������
        {
            moveDirection = new Vector3(input.x, input.y, 0); // ����𷺼ǿ� ���������� x,y��ǥ�� �����ش�.
        }
    }
    void OnClick()
    {
        audioSource.Play();
        Instantiate(bulletObj,buleetPos.transform.position,Quaternion.identity); 
    }
    void OnGunMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        
        if (input != null) //���������� ������
        {
            gunMovePos = new Vector3(input.x, input.y, 0);
        }
    }
    
}
