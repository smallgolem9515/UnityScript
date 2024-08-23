using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timer_text;
    public GameObject Gameover;
    public GameObject Reset_button;
    public GameObject Shop;
    public Transform start_position;
    public PlayerControlrer player_controlrer;
    private bool isdead = false;

    Button button1;
    

    private float time;
    private float time_limit = 60.0f; //타임 리미트는 60초

    private void Awake()
    {
        time = 0;
        Time.timeScale = 1;
        isdead = false;
        Shop.SetActive(false);
    }
    private void Restart()
    {
        player_controlrer.transform.position =
            start_position.transform.position;
        player_controlrer.GetComponent<Rigidbody2D>().velocityX = 0;
    }
    private void Start()
    {
        button1 = Reset_button.GetComponent<Button>();

        button1.onClick.AddListener(Awake);
        button1.onClick.AddListener(Restart);
        
    }

    // Update is called once per frame
    void Update()
    {
        //제한시간보다 작은 시간이라면, 작업 진행
        if (time <= time_limit)
        {
            time += Time.deltaTime; //프레임 당 시간만큼 증가합니다.

            timer_text.text = Mathf.Ceil(time).ToString();
            //소수점 관련 함수
            //Mathf.Round() 반올림
            //Mathf.Ceil() 올림
            //Mathf.Floor() 내림
        }
        else
        {
            Time.timeScale = 0;
            //타임 스케일은 시간이 흐르는 것에 대한 표현
            //기본적으로 1의 값을 가지고 있고
            //수치가 높아지면 그만큼 시간이 빠르게 흘러요.
            isdead = true;
           
        }
        if (isdead == true)
        {
            Gameover.SetActive(true);
        }
        else
            Gameover.SetActive(false);

    }
}
