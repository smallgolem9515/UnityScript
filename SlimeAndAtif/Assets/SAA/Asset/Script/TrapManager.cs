using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject[] Traps1;
    public GameObject[] Traps2;
    Animator animatorTrap1;
    Animator animatorTrap2;
    public float time;
    SpriteRenderer spriteRenderer;
    public Sprite pushedButton;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < Traps1.Length; i++)
        {
            animatorTrap1 = Traps1[i].GetComponent<Animator>();
            animatorTrap1.SetBool("isTrap",true);
        }
        StartCoroutine(TrapOpen());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManagerSlime.instance.isTrapFalse)
        {
            for (int i = 0; i < Traps1.Length; i++)
            {
                animatorTrap1 = Traps1[i].GetComponent<Animator>();
                animatorTrap1.SetBool("isTrap", false);
            }
            for (int i = 0; i < Traps2.Length; i++)
            {
                animatorTrap2 = Traps2[i].GetComponent<Animator>();
                animatorTrap2.SetBool("isTrap", false);
            }
            spriteRenderer.sprite = pushedButton;
        }
        
    }
    IEnumerator TrapOpen()
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < Traps2.Length; i++)
        {
            animatorTrap2 = Traps2[i].GetComponent<Animator>();
            animatorTrap2.SetBool("isTrap", true);
        }
    }
}
