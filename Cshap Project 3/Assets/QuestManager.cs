using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public class Quest
    {
        public string title;
        public string description;

        public Quest(string title, string description)
        {
            this.title = title;
            this.description = description;
        }
    }
    public PlayerControler playerControler; //플레이어 컨트롤러 연결
    public Text quest_text; //퀘스트 출력용 텍스트 연결
    //필드
    private Dictionary<int, Quest> MainQuest = new Dictionary<int, Quest>();

    private void Start()
    {
        playerControler.Quest_number = 0; //퀘스트 초기화

        //딕셔너리명.Add(키,값)을 통해 딕셔너리에 값을 추가할 수 있습니다.
        //퀘스트 추가
        MainQuest.Add(1, new Quest("첫 번째 동행", "앞에 있는 NPC와 대화를 걸어보자."));
        MainQuest.Add(2, new Quest("만나서 반가워", "한번 더 NPC와 대화를 걸어보자."));
        MainQuest.Add(3, new Quest("마무리", "다음 사냥터로 이동하자."));
        //MainQuest[4] = new Quest("dd", "dd"); 이것도 가능
    }

    public void QuestInfo()
    {
        if (MainQuest.ContainsKey(playerControler.Quest_number))
        {
            var quest = MainQuest[playerControler.Quest_number];
            StringBuilder stringBuilder = new StringBuilder(); //스트링 빌더 추가
            //문자열을 조합할 때 새로운 변수를 만들지 않고 결합이 가능한 도구
            stringBuilder.AppendLine($"퀘스트 제목 : {quest.title}").
                AppendLine($"퀘스트 내용 : {quest.description}");
            //AppendLine을 통해 문장을 넣고 띄어쓰기를 진행할 수 있습니다.
            //연속해서 작성이 가능합니다.
            quest_text.text = stringBuilder.ToString();

            //string을 사용하는 경우
            //1. 문자열을 수정하는 경우가 적은 경우
            //2. 문자열을 작성하는 동안 광범위한 검색 작업이 이루어질 때

            //stringBuilder를 사용하는 경우
            //1. 사용자의 입력 등으로 알수 없는 횟수의 문자열을 변경해야하는 경우
            //   그러면서 응용 프로그램 설계 단계에서는 알 수 없는 상황
            //2. 문자열의 변경 횟수가 많은 상황

        }
        else
        {
            quest_text.text = "현재 진행중인 퀘스트는 없습니다.";
        }
    }
    //퀘스트 실행하면 퀘스트런 실행
    public void QuestRun()
    {
        if (playerControler.Quest_number >= 3)
        {
        }
        else
        {
            playerControler.Quest_number++; //퀘스트 번호 변경
            QuestInfo(); //퀘스트에 대한 내용 호출
        }
    }
}
