using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    void Start()
    {
        Dictionary<string, int> ages = new Dictionary<string, int>();
        ages.Add("Alice", 25);
        ages.Add("Bob", 30);
        ages.Add("Charlie", 22);

        Debug.Log(ages["Bob"]);
        Dictionary<string,string> capitals = new Dictionary<string,string>();
        capitals.Add("Korea", "Seoul");
        capitals.Add("Japan", "Tokyo");
        capitals.Add("USA", "Washington D.C");
        foreach(var item in capitals)
            Debug.Log($"{item.Key} | {item.Value}");

        Dictionary<int,string> studentGrades = new Dictionary<int,string>();
        studentGrades.Add(1, "A");
        studentGrades.Add(2, "B");
        studentGrades.Add(3, "A");
        studentGrades.Add(4, "C");

        foreach(var item in studentGrades)
        {
            if(item.Value == "A")
                Debug.Log(item.Key + " | " + item.Value);
        }
        
    }
}
