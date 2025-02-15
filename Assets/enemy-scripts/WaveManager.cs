using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Inst { get; private set; }
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject); // 필요시 추가
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public WaveSO waveSO;
    public int nowWaveCount;
    public GameObject enemySpawnManager;
    public bool isInWave;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(isInWave)ClearCheck();
    }

    //웨이브 매니저에서 업데이트 함수를 하나 만들어서 내거 스크립트에서 변수를 가져가도 될거 같은데

    //지금 신을 이동을 하는 순간에 값을 전달해서 주질 못하는거 같은데 

    public void StartWave()

    {
        EnemySpawnManager.Inst.StartWave();
        isInWave = true;
    }

    public void StopWave()
    {
        EnemySpawnManager.Inst.StopWave();
    }

    public void SetWaveCount(int waveCount)
    {
        nowWaveCount = waveCount;
    }

    public void ClearCheck()
    {
        int countEnemy = 0;

        foreach (Transform child in enemySpawnManager.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                countEnemy++;
            }
        }

        if (countEnemy == 0)
        {
            print("Clear!!!!!!!!!!");
            isInWave = false;
        }
        
    }
    
}