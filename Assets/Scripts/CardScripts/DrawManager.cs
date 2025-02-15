using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Inst { get; private set; }
    
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
    
    public int startCardCount;
    public static Action<bool> onAddCard;
    public bool isLoading;
    public AudioClip drawSound;
    
    
    

    public IEnumerator StartDrawCoroutine()
    {
        startCardCount = GameDataManager.Inst.drawNum;
        isLoading = true;
        CardManager.Inst.nowCost = GameDataManager.Inst.totalCardCost;
        for (int i = 0; i < startCardCount; i++)
        {
            AudioManager.Instance.PlaySFX(drawSound);
            yield return new WaitForSeconds(0.3f);
            onAddCard?.Invoke(true);
        }
        isLoading = false;
        StartCoroutine(StartTurnCoroutine());
    }
 public IEnumerator StartTurnCoroutine()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(11.0f);
            
            isLoading = true;
            //CardManager.Inst.SetupItemBuffer();
            CardManager.Inst.setTurnBuffer();
            CardManager.Inst.nowCost = GameDataManager.Inst.totalCardCost;
            for (int i = 0; i < startCardCount; i++)
            {
                AudioManager.Instance.PlaySFX(drawSound);
                yield return new WaitForSeconds(0.3f);
                onAddCard?.Invoke(true);
            }
            yield return new WaitForSeconds(0.2f);
            isLoading = false;
        }

    }
   
}
