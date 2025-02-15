using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]


[CreateAssetMenu(fileName = "ItemSO",menuName = "Scriptable Objects/ItemSO")]

public class ItemSO : ScriptableObject
{
    public List<Item> items;


}



