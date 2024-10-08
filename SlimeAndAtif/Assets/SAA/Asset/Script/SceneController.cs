using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
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
        OnSceneLoaded("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSceneLoaded(string sceneName) //�� �̸��� �޾Ƽ� BGM�� �����ϴ� �Լ�
    {
        if (SoundManager.instance.BGMaudioSource.isActiveAndEnabled)
        {
            if (sceneName == "GameScene")
            {
                SoundManager.instance.PlayBGM("Feild", 1f);
            }
            else if (sceneName == "Clear")
            {
                SoundManager.instance.PlayBGM("Clear", 1f);
            }
            else if (sceneName == "Menu")
            {
                SoundManager.instance.PlayBGM("Menu", 1f);
            }
            else
            {
                SoundManager.instance.StopBGM();
            }
        }
        //�� �̸��� ���� ��������� ����ϴ� �ڵ�

    }
    public void GameStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //���� ���� ���� �ε����� ��������
        SoundManager.instance.PlaySFX("Click");
        //�Ҹ� ��� ��
        //1�ʵڿ�

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameExit()
    {
        SoundManager.instance.PlaySFX("Click");

        //�Ҹ� ��� ��
        //1�ʵڿ�

        Application.Quit();
    }
}
