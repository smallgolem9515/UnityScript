using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
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
        heartObj[hp].SetActive(false);
        GameObject heart = Instantiate(damagedHeartPrefeb, heartObj[hp].transform.position, Quaternion.identity);
        heart.GetComponent<Animator>().Play("EmptyHeartAni");
        yield return new WaitForSeconds(0.3f);
        Destroy(heart);
        heartObj[hp].SetActive(true);
        heartObj[hp].GetComponent<Image>().sprite = emptyHeart;
    }
}
