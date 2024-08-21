using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//텍스트가 하나하나 단어별로 출력되는 기능을 구현했습니다.
public class TypingText : MonoBehaviour
{
    [Header("타이핑할 텍스트")] public Text text;
    [Header("아이템")] public Item item;
    string message = ""; //출력할 메세지에 대한 정보

    void Start()
    {
        message = item.description; //아이템의 설명을 메세지로 설정
        StartCoroutine(Typing(message)); //코루틴을 동작합니다.
    }
    //코루틴 관련 문법
    //StartCoroutine(IEnumerator 형태의 함수 이름(매개변수명)); 코루틴 작동
    //StopCoroutine(IEnumerator 형태의 함수  이름(매개변수명)); 코루틴 작업 종료
    //StopAllCoroutine(); //모든 코루틴 작업을 종료

    //유니티의 코루틴(Coroutine) : 작업을 진행하다, 일정 시점, 시간 때 탈출 시키고
    //다시 작업으로 돌아오는 처리를 할 수 있게 해주는 문법
    IEnumerator Typing(string message)
    {
        text.text = null; //문장 제거

        //전달받은 메세지의 길이만큼 반복을 진행합니다.
        for (int i = 0; i < message.Length; i++)
        {
            text.text += message[i];
            yield return new WaitForSeconds(0.05f);
        }

        //0.5초 동안 작업 탈출
        //yield는 호출자에게 데이터를 하나씩 리턴하는 작업을 진행 시 사용하는 키워드입니다.
        //yield return : 컬렉션의 데이터를 하나씩 리턴
        //yield break : 리턴을 중지하고 루프를 빠져나가는 용도
    }
    //문장 제거(최초)
    //메세지의 길이만큼 단어 하나하나 추가(텍스트 컴포넌트에)
    //0.5초마다 작업을 다시 진행
}
