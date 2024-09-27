using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text hpText;
    public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"���� ü�� :{PlayerManagerSlime.instance.hp}/{PlayerManagerSlime.instance.maxHP}";
        countText.text = $"������ :{PlayerManagerSlime.instance.count}/{PlayerManagerSlime.instance.maxCount}";
    }
}
