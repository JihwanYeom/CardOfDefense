using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] Transform SpawnPoint;

    void Start()
    {
        StartCoroutine(MakeEnemy());
    }

    void Update()
    {

    }

    IEnumerator MakeEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            Instantiate(Enemy, SpawnPoint);
        }
    }

}
