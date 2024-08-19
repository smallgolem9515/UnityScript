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
        
        player_hp.maxValue = player.max_hp; //�����̴� �ִ� ������ �÷��̾� �ִ� ü��
        player_hp.value = player.hp; // �÷��̾� ü������ �����̴��� ���� ��
    }

    private void Update()
    {
        //GetKeyDown : ������ ��(1��)
        //GetKey : ������ ����
        //GetKeyUp : ���� �� ���� ��(1��)

        if(Input.GetKeyDown(KeyCode.I)) //Ű���� I ��ư�� ������ ��
        {
            if(Player_UI.activeSelf) //�÷��̾�UI�� �����ִٸ�
                Player_UI.SetActive(false); //���ּ���.
            else
                Player_UI.SetActive(true); //���ּ���.
        }
    }
    //����Ƽ c#�� �޼ҵ� ��� ���
    //1. Ű���� �Է� ���� ���� �޼ҵ带 ȣ���մϴ�.

    //2. ����Ƽ�� ��ư ���� Ȱ���� ��ư ������ �� �޼ҵ带 ȣ���մϴ�.
    public void Action01()
    {
        player.Dance();
    }
    public void Action02()
    {
        player.Heal(); // �÷��̾��� ȸ�� ��� ���
        setValue(); //ü�� �ֽ�ȭ

    }
    public void setValue()
    {
        player_hp.value = player.hp;
    }
}
