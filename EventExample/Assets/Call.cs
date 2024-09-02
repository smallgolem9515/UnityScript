using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Call : MonoBehaviour
{
    class Person
    {
        public string Name { get; set; }
    }

    static void ChangeName(ref Person person)
    {
        person = new Person() { Name = "John" };
    }

    static void Main4(string[] args)
    {
        Person person = new Person() { Name = "Alice" };
        ChangeName(ref person);
        Debug.Log(person.Name); // 출력: ?
    }
    static void ModifyArray(int[] arr)
    {
        arr = new int[] { 100, 200, 300 };
    }

    static void Main5(string[] args)
    {
        int[] numbers = { 1, 2, 3 };
        ModifyArray(numbers);
        Debug.Log(numbers[0]); // 출력: ?
    }

    static void Increment(ref int number)
    {
        number += 1;
    }

    static void Main(string[] args)
    {
        int number = 10;
        Increment(ref number);
        Debug.Log(number); // 출력: ?
    }


    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void Main2(string[] args)
    {
        int x = 5;
        int y = 10;
        Swap(ref x, ref y);
        Debug.Log($"x: {x}, y: {y}"); // 출력: ?
    }

    static void MultiplyByTwo(ref int number)
    {
        number *= 2;
    }

    static void Main3(string[] args)
    {
        int number = 5;
        MultiplyByTwo(ref number);
        Debug.Log(number); // 출력: ?
    }
    void Start()
    {
        


    }
   
}
