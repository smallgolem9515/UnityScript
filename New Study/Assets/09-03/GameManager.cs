//using JetBrains.Annotations;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading;
//using Unity.VisualScripting;
//using UnityEngine;

//public delegate bool FilterDele(int num);

//public class GameManager : MonoBehaviour
//{
//    public List<int> EvenNumbers(List<int> numbers, FilterDele filter)
//    {
//        List<int> result = new List<int>();
//        foreach(int num in numbers)
//        {
//            if (filter(num))
//            {
//                result.Add(num);
//            }
//        }
//        return result;
//    }
//    public static int[] DuplicationDelete(int[] nums)
//    {
//        int[] numbers = new int[nums.Length];
//        int e = 0;
//        for (int i = 0; i < nums.Length; i++)
//        {
//            for (int j = 0; j < nums.Length; j++)
//            {
//                if (nums[i] != nums[j])
//                    numbers[i] = nums[i];
//            }
//        }
//        return numbers;
//    }


//    private void Start()
//    {
//        List<int> numbers = new List<int>() { 2, 2, 3, 5, 5, 6 };
//        List<string> names = new List<string>() { "Adalase", "Bob", "Colashawa", "Dave" };
//        int[] nums = numbers.ToArray();
//        nums = DuplicationDelete(nums);
//        foreach(int num in nums)
//        {
//            Debug.Log(num);
//        }
//        //Debug.Log(dele("abc"));
//        //foreach (var num in names)
//        //{
//        //    Debug.Log(num);
//        //}

//    }
//}
