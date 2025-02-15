using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public float time;
    public WaveSO wave;
    
    void Start()
    {
        StartCoroutine(MakeEnemy());
    }
    
    public IEnumerator MakeEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            //Instantiate(Enemy,SpawnPoint);
        }
    }
}
