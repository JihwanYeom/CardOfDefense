using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Objects/Item")]
public class Item: ScriptableObject
{
    public GameObject prefab;
    public Item skillPrefab;
    public int level;
    public Sprite sprite;
    public Item upgrade;
    public Item upgrade2;
    public Item upgrade3;
    public Item upgrade4;
}