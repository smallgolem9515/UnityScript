using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

//해당 스크립트를 컴포넌트로 사용할 경우 반드시 Rigidbody2D가 존재해야 합니다.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlrer : MonoBehaviour
{
    //플레이어 이동 기능 구현
    //유니티에서는 다양한 이동 기능 구현을 위한 데이터가 존재하는데 그중에서
    //리지드바디를 이용한 물리 엔진 제어를 통한 이동을 구현할 것입니다.
    public int speed; //이동 속도
    public int Max_speed; //최대 이동속도

    Rigidbody2D Rigidbody2D; //리지드바디 컴포넌트

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //GetComponent<T>는 해당 T 형태의 컴포넌트를 얻어오는 기능을 가지고 있습니다.
        //따라서 PlayerController를 컴포넌트로 쓰고있는 Circle의 Rigidbody의 
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
        //점프 버튼을 눌렀을 때 점프가 진행되도록 설계
        //Jump는 InputManager에서 Space키로 작동합니다.
        if(Input.GetButtonDown("Jump"))
        {
            Rigidbody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //유니티에는 Input Manager가 존재합니다.
        //유니티에서 사용하는 입력에 관련된 정보들이 저장되어있습니다.
        //그중에서 Axies를 불러오는 문법이 GetAxis
        //그 결과를-1, 0, 1로 가져오는 문법이 GetAxisRaw로 주로 1칸 이동에 대한 구현 시 사용합니다.
        //Horiziontal axies 중에서 가로에 대한 입력입니다.

        Rigidbody2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //AddForse는 방향과 힘 모드를 통해 힘을 가하는 문법
        //유니티에서 방향은 Vector2를 통해 2d x축과 y축의 좌표를 표현합니다.
        //                  Vector3를 통해 x, y, z축의 좌표를 표현합니다.

        //힘 모드는 물체에 힘을 가하는 것에 대한 설정
        //2D 기준 Impulse, Force로 나뉘며 순간적인 힘이나 지속적인 힘이냐로 갈립니다.

        //물체의 x좌표 속력이 설정해둔 최대 스피드보다 빠르다면
        if (Rigidbody2D.velocity.x > Max_speed)
        {
            //오른쪽 이동 기준
            Rigidbody2D.velocity = new Vector2(Max_speed, Rigidbody2D.velocity.y);
            //속력을 최대 속도로 설정합니다.
        }
        else if (Rigidbody2D.velocity.x < -1 * Max_speed)
        {
            //왼쪽 이동 기준
            Rigidbody2D.velocity = new Vector2(-1 * Max_speed, Rigidbody2D.velocity.y);
        }
    }
}
