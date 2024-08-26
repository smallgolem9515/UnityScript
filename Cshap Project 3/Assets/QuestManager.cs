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
    public PlayerControler playerControler; //�÷��̾� ��Ʈ�ѷ� ����
    public Text quest_text; //����Ʈ ��¿� �ؽ�Ʈ ����
    //�ʵ�
    private Dictionary<int, Quest> MainQuest = new Dictionary<int, Quest>();

    private void Start()
    {
        playerControler.Quest_number = 0; //����Ʈ �ʱ�ȭ

        //��ųʸ���.Add(Ű,��)�� ���� ��ųʸ��� ���� �߰��� �� �ֽ��ϴ�.
        //����Ʈ �߰�
        MainQuest.Add(1, new Quest("ù ��° ����", "�տ� �ִ� NPC�� ��ȭ�� �ɾ��."));
        MainQuest.Add(2, new Quest("������ �ݰ���", "�ѹ� �� NPC�� ��ȭ�� �ɾ��."));
        MainQuest.Add(3, new Quest("������", "���� ����ͷ� �̵�����."));
        //MainQuest[4] = new Quest("dd", "dd"); �̰͵� ����
    }

    public void QuestInfo()
    {
        if (MainQuest.ContainsKey(playerControler.Quest_number))
        {
            var quest = MainQuest[playerControler.Quest_number];
            StringBuilder stringBuilder = new StringBuilder(); //��Ʈ�� ���� �߰�
            //���ڿ��� ������ �� ���ο� ������ ������ �ʰ� ������ ������ ����
            stringBuilder.AppendLine($"����Ʈ ���� : {quest.title}").
                AppendLine($"����Ʈ ���� : {quest.description}");
            //AppendLine�� ���� ������ �ְ� ���⸦ ������ �� �ֽ��ϴ�.
            //�����ؼ� �ۼ��� �����մϴ�.
            quest_text.text = stringBuilder.ToString();

            //string�� ����ϴ� ���
            //1. ���ڿ��� �����ϴ� ��찡 ���� ���
            //2. ���ڿ��� �ۼ��ϴ� ���� �������� �˻� �۾��� �̷���� ��

            //stringBuilder�� ����ϴ� ���
            //1. ������� �Է� ������ �˼� ���� Ƚ���� ���ڿ��� �����ؾ��ϴ� ���
            //   �׷��鼭 ���� ���α׷� ���� �ܰ迡���� �� �� ���� ��Ȳ
            //2. ���ڿ��� ���� Ƚ���� ���� ��Ȳ

        }
        else
        {
            quest_text.text = "���� �������� ����Ʈ�� �����ϴ�.";
        }
    }
    //����Ʈ �����ϸ� ����Ʈ�� ����
    public void QuestRun()
    {
        if (playerControler.Quest_number >= 3)
        {
        }
        else
        {
            playerControler.Quest_number++; //����Ʈ ��ȣ ����
            QuestInfo(); //����Ʈ�� ���� ���� ȣ��
        }
    }
}
