using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneContoller : MonoBehaviour
{
    public static SceneContoller instance;

    public Button gameStartButton;
    public Button gameQuitButton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Logo")
        {
            StartCoroutine(LogoStart());
            StartCoroutine(MenuSceneButton());
        }   
    }
    public void OnSceneLoaded(string sceneName) //�� �̸��� �޾Ƽ� BGM�� �����ϴ� �Լ�
    {
        if(Soundsmanager.Instance.BGMaudioSourse.isActiveAndEnabled)
        {
            if (sceneName == "GameScene")
            {
                Soundsmanager.Instance.PlayBGM("Level1", 1f);
            }
            else if (sceneName == "GameScene2")
            {
                Soundsmanager.Instance.PlayBGM("Level2", 1f);
            }
            else if (sceneName == "Menu")
            {
                Soundsmanager.Instance.PlayBGM("Menu", 1f);
            }
            else
            {
                Soundsmanager.Instance.StopBGM();
            }
        }
        //�� �̸��� ���� ��������� ����ϴ� �ڵ�
        
    }
    IEnumerator LogoStart()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Menu");
    }
    IEnumerator MenuSceneButton()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject startButtonObj = GameObject.Find("StartButton");
        GameObject qutiButtonObj = GameObject.Find("ExitButton");
        if (startButtonObj != null)
        {
            gameStartButton = startButtonObj.GetComponent<Button>();
            gameStartButton.onClick.AddListener(GameStart);
        }
        if (qutiButtonObj != null)
        {
            gameQuitButton = qutiButtonObj.GetComponent<Button>();
            gameQuitButton.onClick.AddListener(GameExit);
        }
        OnSceneLoaded(SceneManager.GetActiveScene().name);
    }
    public void GameStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //���� ���� ���� �ε����� ��������

        //�Ҹ� ��� ��
        //1�ʵڿ�

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameExit()
    {

        //�Ҹ� ��� ��
        //1�ʵڿ�

        Application.Quit();
    }
}
