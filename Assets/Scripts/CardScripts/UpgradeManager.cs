using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public ItemSO DeckSO;
    public ItemSO itemSO;
    public static UpgradeManager Inst { get; private set; }
    
    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            ResetDeckSO();
            DontDestroyOnLoad(gameObject); // 필요시 추가
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    
    public void ResetDeckSO()
    {
        DeckSO.items = new List<Item>();
        int baseCardCount = 6;
        for (int i = 0; i < baseCardCount; i++)
        {
            DeckSO.items.Add(itemSO.items[0]);
        }
    }
    
    public void Upgrade(Item nowCard,Item upgradedCard)
    {
        if (nowCard.level <= GameDataManager.Inst.upgradeCost)
        {
            // uCard=uCard.upgrade;
            int count = DeckSO.items.Count;
            for (int i = 0; i < count; i++)
            {
                if (DeckSO.items[i].name == nowCard.name)
                {
                    DeckSO.items[i] = upgradedCard;
                    GameDataManager.Inst.upgradeCost-=nowCard.level;
                    break;
                }
            }
        }
    }
    
    
}
