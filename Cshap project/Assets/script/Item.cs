using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "SO/Item")]
public class Item : ScriptableObject
{
    public string name;
    public string description;
    public int count;
}
