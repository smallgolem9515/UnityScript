using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public Text chat_text;
    private Dictionary<int, string> Chat_L = new Dictionary<int, string>();
    public int number;
    public GameObject chat_panal;
    

    // Start is called before the first frame update
    void Start()
    {
        number = 1;

        Chat_L[1] = new string("qqq");
        Chat_L[2] = new string("www");
        Chat_L[3] = new string("eee");
        Chat_L[4] = new string("rrr");
    }

    public void Chat_Info()
    {
        if (number == 4)
            number = 1;
        else
        {
            number++;
            Chat_L.ContainsKey(number);
            chat_text.text = Chat_L[number];
        }
    }
    public void Chat_ture()
    {
        if (number == 1)
        {
            chat_panal.SetActive(true);

        }
        else if (number == 4) 
        {
            chat_panal.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
