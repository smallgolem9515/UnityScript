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
    private float time_limit = 60.0f; //Ÿ�� ����Ʈ�� 60��

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
        //���ѽð����� ���� �ð��̶��, �۾� ����
        if (time <= time_limit)
        {
            time += Time.deltaTime; //������ �� �ð���ŭ �����մϴ�.

            timer_text.text = Mathf.Ceil(time).ToString();
            //�Ҽ��� ���� �Լ�
            //Mathf.Round() �ݿø�
            //Mathf.Ceil() �ø�
            //Mathf.Floor() ����
        }
        else
        {
            Time.timeScale = 0;
            //Ÿ�� �������� �ð��� �帣�� �Ϳ� ���� ǥ��
            //�⺻������ 1�� ���� ������ �ְ�
            //��ġ�� �������� �׸�ŭ �ð��� ������ �귯��.
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
