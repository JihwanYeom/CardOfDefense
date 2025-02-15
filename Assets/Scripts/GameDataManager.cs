using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    
    public int totalCardCost;
    public int upgradeCost;
    public int drawNum;
    
    public bool is_done = true; // true 일 경우에 map scene에서 선택지 표시와 동시에 버튼 누르면 false로 바뀌고 게임 종료시에 true로 바뀜
    
    public static GameDataManager Inst { get; private set; }
    
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
