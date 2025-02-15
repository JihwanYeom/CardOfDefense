using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDeck : MonoBehaviour
{
    public ItemSO DeckSO;
    public ItemSO itemSO;
    // Start is called before the first frame update
    void Start()
    {
        ResetDeckSO();
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

}
