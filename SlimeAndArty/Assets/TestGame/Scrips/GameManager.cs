using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseMenu;
    private bool isPasue = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPasue)
            {
                PasueGame();
            }
            else
            {
                OnContinue();
            }
        }
    }
    public void OnRestart()
    {
        Soundsmanager.Instance.PlaySFX("Empty");
        pauseMenu.SetActive(false);
        isPasue = false ;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("재시작");
    }
    public void PasueGame()
    {
        Soundsmanager.Instance.PlaySFX("Empty");
        isPasue = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("PasueGame");
    }
    public void OnContinue()
    {
        Soundsmanager.Instance.PlaySFX("Empty");
        isPasue =false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("다시하기");
    }
    public void OnQuit()
    {
        Soundsmanager.Instance.PlaySFX("Empty");
        Debug.Log("나가기");
        Application.Quit();
    }
}

