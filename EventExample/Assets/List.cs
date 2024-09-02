using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    void Start()
    {
        List<int> numbers1 = new List<int>() { 3, 7, 11, 18, 25 };
        List<string> fruits = new List<string>() { "apple", "banana", "orange" };
        List<int> numbers2 = new List<int>() { 10, 20, 30, 40, 50 };

        
        //for (int i = 0; i < numbers1.Count; i++)
        //{
        //    for (int j = 0; i < numbers1.Count; i++)
        //    {
        //        if (numbers1[i] < numbers1[j + 1])
        //        {
        //            int temp = numbers1[j + 1];
        //            numbers1[j + 1] = numbers1[i];
        //            numbers1[i] = temp;
        //        }
        //    }      
        //}
        numbers1.Reverse();
        foreach (int i in numbers1)
        {
            Debug.Log(i);
        }
        foreach(string i in fruits)
            Debug.Log(i);
        fruits.Insert(0, "grape");
        foreach (string i in fruits)
            Debug.Log(i);
        int result = 0;
        for (int i = 0; i < numbers2.Count; i++)
        {
            if(i % 2 == 0)
                result += numbers2[i];

        }
        Debug.Log(result);








    }

    
}
