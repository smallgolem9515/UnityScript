using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    float speed = 3.0f;
    Vector3 moveDirection; //����3 ����
    public GameObject defaultObj;
    AudioSource diyingAudioSource;
    public GameObject gameOver;
    private void Start()
    {
        diyingAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (moveDirection.x > 0) //�������� x��ǥ�� 0���� ũ�ٸ�
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // �����̼��� y���� ����
            //transform.localScale = new Vector3(5, 5, 0); // ������ ���� ����
        }
        else if(moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
        }
        transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        //Mathf.Abs�� ���� float���� ���밪�� return�Ѵ�.
        //Vector3(x,y,z)�� ������ ������ �� ��ǥ�� �� �־�����.
        //Debug.Log(moveDirection.x);
        //�� Ű�� ������ ��0.7���� 1������ ���� �־�����. �ű⿡ 3.0*�������� �������� �������� �ӵ��� ���´�.
    }
    void OnMovement(InputValue value) //On + "Actions name" �Լ��̸����� ����
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //���������� ������
        {
            moveDirection = new Vector3(input.x, input.y, 0); // ����𷺼ǿ� ���������� x,y��ǥ�� �����ش�.
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster"
            || collision.gameObject.tag == "Spike")
        {
            diyingAudioSource.Play();
            Respon();
            gameOver.SetActive(true);
            Invoke("RectiveateGameOver", 0.3f);
        }
        
    }
    public void Respon()
    {
        transform.position = defaultObj.transform.position;

    }
    void RectiveateGameOver()
    {
        gameOver.SetActive(false);
    }
}
