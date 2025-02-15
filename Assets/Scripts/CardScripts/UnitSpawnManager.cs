using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    
    public static UnitSpawnManager Inst { get; private set; }
    private void Awake()
    {
        if (Inst == null)
        {   
            Inst = this;
            //DontDestroyOnLoad(gameObject); // 필요시 추가
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public bool SpawnUnit(Vector3 spawnPos,Card card)
    {
        
        Instantiate(card.cardObj, spawnPos, Quaternion.identity);
        return true;
    }
}
