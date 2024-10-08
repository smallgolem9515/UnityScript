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
    public void OnSceneLoaded(string sceneName) //씬 이름을 받아서 BGM을 설정하는 함수
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
        //씬 이름에 따라 배경음악을 재생하는 코드

    }
    public void GameStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //현재 씬의 빌드 인덱스를 가져오기
        SoundManager.instance.PlaySFX("Click");
        //소리 재생 후
        //1초뒤에

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void GameExit()
    {
        SoundManager.instance.PlaySFX("Click");

        //소리 재생 후
        //1초뒤에

        Application.Quit();
    }
}
