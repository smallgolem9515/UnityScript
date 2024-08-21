using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Tooltip("작업할 아이템으로 Item 폴더 쪽의 데이터를 넣어주세요.")]
    public Item item; //아이템에 대한 작업 추가

    [SerializeField]
    private Text AppleText;
    [SerializeField]
    private Text ItemcountText;
    //private인 데이터를 인스펙터에서는 읽을 수 있도록 처리해줍니다.
    //데이트의 직렬화(Serialize)
    //오브젝트나 데이터를 다른 환경에서도 사용할 수 있게 해주는 작업

    //아이템 사용 방법
    //1. public을 이용한 직접적인 등록
    //2. 유니티 경로를 추적해서 코드를 통한 등록
    //이번 코드에는 1번만 작업

    private void Start()
    {
        SetText();
        SetItemName();
    }
    public void SetItemName()
    {
        AppleText.text = item.name;
    }
    public void Plus()
    {
        item.count++;
        SetText();
    }
    public void Minus()
    {
        item.count--;
        SetText();
    }
    public  void SetText()
    {
        ItemcountText.text = $"현재 사과 보유 개수 : {item.count : #,##0}개";
    }
}
