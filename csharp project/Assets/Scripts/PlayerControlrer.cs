using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

//�ش� ��ũ��Ʈ�� ������Ʈ�� ����� ��� �ݵ�� Rigidbody2D�� �����ؾ� �մϴ�.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlrer : MonoBehaviour
{
    //�÷��̾� �̵� ��� ����
    //����Ƽ������ �پ��� �̵� ��� ������ ���� �����Ͱ� �����ϴµ� ���߿���
    //������ٵ� �̿��� ���� ���� ��� ���� �̵��� ������ ���Դϴ�.
    public int speed; //�̵� �ӵ�
    public int Max_speed; //�ִ� �̵��ӵ�

    Rigidbody2D Rigidbody2D; //������ٵ� ������Ʈ

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //GetComponent<T>�� �ش� T ������ ������Ʈ�� ������ ����� ������ �ֽ��ϴ�.
        //���� PlayerController�� ������Ʈ�� �����ִ� Circle�� Rigidbody�� 
    }
    // Main Logic
    void Update()
    {
        Move();
        Jump();
    }
    public float jump;
    void Jump()
    {
        //���� ��ư�� ������ �� ������ ����ǵ��� ����
        //Jump�� InputManager���� SpaceŰ�� �۵��մϴ�.
        if(Input.GetButtonDown("Jump"))
        {
            Rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //����Ƽ���� Input Manager�� �����մϴ�.
        //����Ƽ���� ����ϴ� �Է¿� ���õ� �������� ����Ǿ��ֽ��ϴ�.
        //���߿��� Axies�� �ҷ����� ������ GetAxis
        //�� �����-1, 0, 1�� �������� ������ GetAxisRaw�� �ַ� 1ĭ �̵��� ���� ���� �� ����մϴ�.
        //Horiziontal axies �߿��� ���ο� ���� �Է��Դϴ�.

        Rigidbody2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //AddForse�� ����� �� ��带 ���� ���� ���ϴ� ����
        //����Ƽ���� ������ Vector2�� ���� 2d x��� y���� ��ǥ�� ǥ���մϴ�.
        //                  Vector3�� ���� x, y, z���� ��ǥ�� ǥ���մϴ�.

        //�� ���� ��ü�� ���� ���ϴ� �Ϳ� ���� ����
        //2D ���� Impulse, Force�� ������ �������� ���̳� �������� ���̳ķ� �����ϴ�.

        //��ü�� x��ǥ �ӷ��� �����ص� �ִ� ���ǵ庸�� �����ٸ�
        if (Rigidbody2D.velocity.x > Max_speed)
        {
            //������ �̵� ����
            Rigidbody2D.velocity = new Vector2(Max_speed, Rigidbody2D.velocity.y);
            //�ӷ��� �ִ� �ӵ��� �����մϴ�.
        }
        else if (Rigidbody2D.velocity.x < -1 * Max_speed)
        {
            //���� �̵� ����
            Rigidbody2D.velocity = new Vector2(-1 * Max_speed, Rigidbody2D.velocity.y);
        }
    }
}
