using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="item", menuName = "GA/Item")]
public class Item : ScriptableObject
{
    public string name;
    public int money;
    public int price;
    public int count;
}
