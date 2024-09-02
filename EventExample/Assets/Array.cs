
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Array : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] number = { 2, 3, 4, 5, 6, 7, 8, 9, };
        int[] number2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
        int[] result = new int[number2.Length];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                result[j] = number[i] * number2[j];
                Debug.Log($"{number[i]} * {number2[j]} = {result[j]}");

            }
            Debug.Log("");
        }
        int[] number3 = { 5, 8, 12, 15, 20 };
        int[] number4 = { 2, 4, 6, 8, 10 };
        string[] words = { "apple", "banana","cherry","date"};

        int result2 = 0;
        for (int i = 0; i < number3.Length; i++)
        {
            result2 += number[i]; 
        }
        Debug.Log(result2);

        for (int i = 0;i < number4.Length; i++)
        {
            number4[i] *= 2;
        }

        foreach (int i in number4)
        {
            Debug.Log(i);
        }

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length >= 5)
                Debug.Log(words[i]);
        }
        int[] number5 = new int[5];
        for (int i = 0;i<number5.Length;i++)
        {
            number5[i] = UnityEngine.Random.Range(1, 11);
        }
        RandomFiveSort(number5);
        RandomFiveReverse(number5);

    }
    public static void RandomFiveSort(int[] x)
    {
        List<int> x_list = new List<int>();
        for (int i = 0; i < x.Length; i++)
        {
            x_list.Add(x[i]);
        }
        x_list.Sort();
        foreach (int i in x_list)
            Debug.Log(i);
    } 
    public static void RandomFiveReverse(int[] x)
    {
        List<int> x_list = new List<int>();
        for (int i = 0; i < x.Length; i++)
        {
            x_list.Add(x[i]);
        }
        x_list.Sort();
        x_list.Reverse();
        foreach (int i in x_list)
            Debug.Log(i);
    }


}
