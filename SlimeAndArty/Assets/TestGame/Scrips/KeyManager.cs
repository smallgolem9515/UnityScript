using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance {  get; private set; }
    List<string> key1List = new List<string>();
    public bool door1 = false;
    Dictionary<GameObject,List<int>> keyDoorSet = new Dictionary<GameObject,List<int>>();

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            door1 = true;
            gameObject.SetActive(false);
        }
    }
}
