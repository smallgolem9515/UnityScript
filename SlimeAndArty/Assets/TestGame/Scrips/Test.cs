using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Slider hpSlider;
    public int maxHP = 100;
    private int currentHP;

    public Image fillImage;

    public Vector3 hpBarOffset = new Vector3(0,2.0f,0);

    public Text scoreText;
    private int score = 0;
    private Color originalColor;
    SpriteRenderer SpriteRenderer;
    public Sprite changeImage;
    private void Start()
    {
        //currentHP = maxHP; //체력 초기화
        //hpSlider.maxValue = maxHP; //HP바의 최대치는 최대체력으로
        //hpSlider.value = currentHP; //
        hpSlider.maxValue = 500;
        hpSlider.value = 0;
        UPdateScoreText();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = scoreText.color;
    }
    private void Update()
    {
        hpSlider.transform.position = transform.position + hpBarOffset;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("발사");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);
            
            if(hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("충돌");
                //TakeDamage(10);
                IncreaseScore(10);
            }
        }
        
    }
    void UPdateScoreText()
    {
        scoreText.text = "Score : " + score;
    }
    void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP < 0) currentHP = 0;

        hpSlider.value = currentHP;

        if(currentHP == 0)
        {
            Debug.Log("Player Die!!!");
        }
    }
    void IncreaseScore(int amount)
    {
        hpSlider.value += amount;
        score += amount;
        UPdateScoreText();
        StartCoroutine(FlashRed());
        if (hpSlider.value == 500)
        {
            SpriteRenderer.sprite = changeImage;
            hpSlider.value = 0f;
        }
    }
    IEnumerator FlashRed()
    {
        scoreText.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        scoreText.color = originalColor;
    }
}
