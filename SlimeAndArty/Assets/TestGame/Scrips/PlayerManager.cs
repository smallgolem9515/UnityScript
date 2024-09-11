using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    float speed = 3.0f;
    Vector3 moveDirection; //벡터3 정의
    public GameObject defaultObj;
    AudioSource diyingAudioSource;
    public GameObject gameOver;
    private void Start()
    {
        diyingAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (moveDirection.x > 0) //눌렀을때 x좌표가 0보다 크다면
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // 로테이션의 y값을 변경
            //transform.localScale = new Vector3(5, 5, 0); // 스케일 값을 변경
        }
        else if(moveDirection.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            //transform.localScale = new Vector3(-5, 5, 0);
        }
        transform.Translate(new Vector3(Mathf.Abs(moveDirection.x),moveDirection.y,0)*Time.deltaTime*speed);
        //Mathf.Abs는 넣은 float값의 절대값을 return한다.
        //Vector3(x,y,z)에 계산식을 넣을시 각 좌표에 다 넣어진다.
        //Debug.Log(moveDirection.x);
        //각 키를 누를시 약0.7에서 1까지의 값이 넣어진다. 거기에 3.0*프레임을 넣은값이 곱해져서 속도가 나온다.
    }
    void OnMovement(InputValue value) //On + "Actions name" 함수이름으로 지정
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null) //눌러진값이 있을때
        {
            moveDirection = new Vector3(input.x, input.y, 0); // 무브디렉션에 눌렀을때의 x,y좌표를 보내준다.
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
