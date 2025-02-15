using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject player;
    public static EnemySpawnManager Inst { get; private set; }
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public WaveSO waveSO;
    public int nowWaveCount;
    private Transform _transform;
    private GameObject nowBackGround;
    private Coroutine coroutine1;
    private Coroutine coroutine2;
    private Coroutine coroutine3;
    
    public int check=0;
    // Start is called before the first frame update
    void Start()
    {
        nowWaveCount = WaveManager.Inst.nowWaveCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //웨이브 매니저에서 업데이트 함수를 하나 만들어서 내거 스크립트에서 변수를 가져가도 될거 같은데

		//지금 신을 이동을 하는 순간에 값을 전달해서 주질 못하는거 같은데 
    public IEnumerator MakeEnemy1()
    {
        float time = waveSO.waves[nowWaveCount].spawnTime1;
        GameObject monster1= waveSO.waves[nowWaveCount].spawnMonster1;
        for(int i=0;i<waveSO.waves[nowWaveCount].spawnCount1;i++)
        {
            yield return new WaitForSeconds(time);
            Instantiate(monster1,_transform);
        }

        coroutine1 = null;
    }
    
    public IEnumerator MakeEnemy2()
    {
        float time = waveSO.waves[nowWaveCount].spawnTime2;
        GameObject monster2= waveSO.waves[nowWaveCount].spawnMonster2;
        for(int i=0;i<waveSO.waves[nowWaveCount].spawnCount2;i++)
        {
            yield return new WaitForSeconds(time);
            Instantiate(monster2,_transform);
        }
        coroutine2 = null;
    }
    
    public IEnumerator MakeEnemy3()
    {
        float time = waveSO.waves[nowWaveCount].spawnTime3;
        GameObject monster3= waveSO.waves[nowWaveCount].spawnMonster3;
        for(int i=0;i<waveSO.waves[nowWaveCount].spawnCount3;i++)
        {
            yield return new WaitForSeconds(time);
            Instantiate(monster3,_transform);
        }

        coroutine3 = null;
    }

    public IEnumerator CheckWave()
    {
        while (true)
        {
            check = CheckFinishWave();
            if (check==1)
            {
                print("Clear!!!");
                //StopWave();
                break;
            }
            print("No Clear");
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void StartWave()
    {
        nowBackGround = Instantiate(waveSO.waves[nowWaveCount].backGround);
        
        _transform = GetComponent<Transform>();
        
       
        
        if (waveSO.waves[nowWaveCount].spawnMonster1 != null)
            coroutine1 = StartCoroutine(MakeEnemy1());
        
        if (waveSO.waves[nowWaveCount].spawnMonster2 != null)
            coroutine2 = StartCoroutine(MakeEnemy2());
        
        if (waveSO.waves[nowWaveCount].spawnMonster3 != null)
            coroutine3 = StartCoroutine(MakeEnemy3());
        
        //Invoke("StopWave",5.0f);
    }

    public void StopWave()
    {
        //Destroy(nowBackGround);
        if (coroutine1 != null)
        {
            StopCoroutine(coroutine1);
            coroutine1 = null;
        }


        if (coroutine2 != null)
        {
            StopCoroutine(coroutine2);
            coroutine2 = null;
        }

        if (coroutine3 != null)
        {
            StopCoroutine(coroutine3);
            coroutine3 = null;
        }
    }

    public int CheckFinishWave()
    {
        if (coroutine1 == null && coroutine2 == null && coroutine3 == null)
        {
            int monsterNum = transform.childCount;
            if (monsterNum == 0)
            {
                
                return 1;
            }
            
        }
        
        return 0;
    }
}
