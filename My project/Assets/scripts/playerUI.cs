using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    public player player;

    public Image player_Image;
    public Slider player_hp;
    public Text player_name;

    public GameObject Player_UI;

    private void Start()
    {
        player_Image.sprite = player.image;
        player_name.text = player.name;
        
        player_hp.maxValue = player.max_hp; //슬라이더 최대 범위를 플레이어 최대 체력
        player_hp.value = player.hp; // 플레이어 체력으로 슬라이더의 현재 값
    }

    private void Update()
    {
        //GetKeyDown : 눌렀을 때(1번)
        //GetKey : 누르는 동안
        //GetKeyUp : 누른 걸 뗏을 때(1번)

        if(Input.GetKeyDown(KeyCode.I)) //키보드 I 버튼을 눌렀을 때
        {
            if(Player_UI.activeSelf) //플레이어UI가 켜져있다면
                Player_UI.SetActive(false); //꺼주세요.
            else
                Player_UI.SetActive(true); //켜주세요.
        }
    }
    //유니티 c#의 메소드 사용 방법
    //1. 키보드 입력 값에 따라 메소드를 호출합니다.

    //2. 유니티의 버튼 등을 활용해 버튼 눌렀을 때 메소드를 호출합니다.
    public void Action01()
    {
        player.Dance();
    }
    public void Action02()
    {
        player.Heal(); // 플레이어의 회복 기능 사용
        setValue(); //체력 최신화

    }
    public void setValue()
    {
        player_hp.value = player.hp;
    }
}
