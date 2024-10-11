using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
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
    public Text countText;

    public GameObject[] heartObj;
    public GameObject damagedHeartPrefeb;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public GameObject pauseMenu;
    private bool isPasue = false;

    void Start()
    {
        for (int i = 0; i < PlayerManagerSlime.instance.limitHP; i++)
        {
            if (i < PlayerManagerSlime.instance.maxHP)
            {
                heartObj[i / 2].SetActive(true);
            }
            else
            {
                heartObj[i / 2].SetActive(false);
            }
        }
        pauseMenu.SetActive(false);

    }
    void Update()
    {      
        countText.text = $"ÀåÀü¼ö :{PlayerManagerSlime.instance.count}/{PlayerManagerSlime.instance.maxCount}";
    }
    public IEnumerator HalfHeart(int hp)
    {
        heartObj[hp].SetActive(false);
        GameObject heart = Instantiate(damagedHeartPrefeb, heartObj[hp].transform.position, Quaternion.identity);
        heart.GetComponent<Animator>().Play("HalfHeartAni");
        yield return new WaitForSeconds(0.3f);
        Destroy(heart);
        heartObj[hp].SetActive(true);
        heartObj[hp].GetComponent<Image>().sprite = halfHeart;
    }
    public IEnumerator EmptyHeart(int hp)
    {
        if(hp != 0)
        {
            heartObj[hp].SetActive(false);
            GameObject heart = Instantiate(damagedHeartPrefeb, heartObj[hp].transform.position, Quaternion.identity);
            heart.GetComponent<Animator>().Play("EmptyHeartAni");
            yield return new WaitForSeconds(0.3f);
            Destroy(heart);
            heartObj[hp].SetActive(true);
            heartObj[hp].GetComponent<Image>().sprite = emptyHeart;
        }
        
    }
    public void HeartUIRecovery()
    {
        for (int i = 0; i < PlayerManagerSlime.instance.maxHP/2; i++)
        {
              heartObj[i].GetComponent<Image>().sprite = fullHeart;
        }
    }
    void OnPauseMenu()
    {
        if (!isPasue)
        {
            PauseGame();
        }
        else
        {
            OnContinue();
        }
    }
    public void PauseGame()
    {
        isPasue = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("PauseMenu");
    }
    public void OnContinue()
    {
        isPasue=false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Continue");
    }
    public void OnRestart()
    {
        isPasue = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        PlayerManagerSlime.instance.Respon();
        SceneManager.LoadScene("GameScene");
        Debug.Log("ReStart");
    }
    public void OnQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
