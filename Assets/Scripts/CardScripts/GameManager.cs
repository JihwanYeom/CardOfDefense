using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //게임 매니저 로드 하는 타이밍 ( 각 씬 시작할때 )- 한 스테이지 시작하는 경우에 변수들 로드해서 사용 / 맵뷰에서 업그레이드 가능한 횟수를 가져와 사용
    //게임 매니저 업데이트 하는 타이밍 ( 게임 씬 종료할 때 ) - 한 스테이지 종료시에 선택 할 수 있는 선택지를 선택해서 변수내용을 바꿔야하는
    
    //모두 public
    //추가해야할 내용
    
    //총 토탈 비용 (카드 매니저에서 카드 최대 사용 제한)
    //한번에 드로우하는 카드 수 (드로우 매니저에서 값 가져가 사용)
    //업그레이드 횟수 (맵 뷰에서 업그레이드 횟수 표시 / 제한)
    
    
    
    
    public static GameManager Inst { get; private set; }
    
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
    // Start is called before the first frame update
    void Start()
    {
        if (WaveManager.Inst.nowWaveCount == 0)
        {
            //CardManager.Inst.ResetDeckSO();
        }
        WaveManager.Inst.StartWave();
        StartGame();
        StartCoroutine(EnemySpawnManager.Inst.CheckWave());
    }



    // Update is called once per frame
    void Update()
    {
        //InputKey();
        //EnemySpawnManager.Inst.CheckFinishWave();
    }

    public void StartGame()
    {
        StartCoroutine(DrawManager.Inst.StartDrawCoroutine());
    }

    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DrawManager.Inst.StartTurnCoroutine());
        }
    }
    
}
