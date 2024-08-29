using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    public void CreateObject()
    {
        Instantiate(objects[0]);
    }
    

    private void Start()
    {
        Invoke("CreateObject", 2);
        InvokeRepeating("CreateObject",2, 0.3f);
    }


}
