using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class create : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<Vector3> transforms = new List<Vector3>();

    

    public void CreateObject()
    {
        var random_object = objects[Random.Range(0, objects.Count)];
        var random_position = transforms[Random.Range(0, transforms.Count)];
        Instantiate(random_object, random_position, Quaternion.identity);
    }
    

    private void Start()
    {
        Invoke("CreateObject", 2);
        InvokeRepeating("CreateObject",2, 1.0f);
    }


}
