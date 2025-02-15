using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]


[CreateAssetMenu(fileName = "WaveSO",menuName = "Scriptable Objects/WaveSO")]

public class WaveSO : ScriptableObject
{
    public List<Wave> waves;
}