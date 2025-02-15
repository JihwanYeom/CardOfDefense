using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "Wave",menuName = "Scriptable Objects/Wave")]
public class Wave: ScriptableObject
{
    public float spawnTime1;
    public float spawnTime2;
    public float spawnTime3;
    public float spawnTime4;
    public int spawnCount1;
    public int spawnCount2;
    public int spawnCount3;
    public int spawnCount4;
    public GameObject spawnMonster1;
    public GameObject spawnMonster2;
    public GameObject spawnMonster3;
    public GameObject spawnMonster4;
    public GameObject backGround;
}
