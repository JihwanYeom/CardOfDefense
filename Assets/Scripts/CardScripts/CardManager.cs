
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
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
    
    public ItemSO itemSO;
    public ItemSO DeckSO;
    public List<Card> myCards;
    public List<Item> usedCards;
    public List<Item> myDeck;
    public List<Item> handCards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform LeftCard;
    [SerializeField] Transform RightCard;
    
    
    [SerializeField] List<Item> itemBuffer;
    Card selectedCard;
    public bool isCardDrawing;
    public bool onMyCardArea;
    public int nowCost;
    public AudioClip useCardSound;
    void Start()
    {
        SetupItemBuffer();
        DrawManager.onAddCard += AddCard;
        
    }

    void Update()
    {
        if (isCardDrawing)
        {
            CardDrag();
        }    
    }
    
    //DeckSO초기화
    public void ResetDeckSO()
    {
        DeckSO.items = new List<Item>();
        int baseCardCount = 6;
        for (int i = 0; i < baseCardCount; i++)
        {
            DeckSO.items.Add(itemSO.items[0]);
        }
    }
    //카드뽑기
    public Item PopItem()
    {
        if (myDeck.Count == 0)
        {
            SetupItemBuffer();
        }
        Item item = myDeck[0];
        myDeck.RemoveAt(0);
        return item;
        
    }
    
    
    
    //덱 생성 및 초기화
    public void SetupItemBuffer()
    {
        //아이템들 일괄 추가
        //itemBuffer = new List<Item>(100);
        myDeck.Clear();
        
        for (int i = 0; i < DeckSO.items.Count; i++)
        {
            Item item = DeckSO.items[i];
            myDeck.Add(item);
            if(item.skillPrefab!=null)myDeck.Add(item.skillPrefab);
        }
        
        for (int i = 0; i < myDeck.Count; i++)
        {
            int rand = Random.Range(0, myDeck.Count);
            Item temp = myDeck[i];
            myDeck[i] = myDeck[rand];
            myDeck[rand] = temp;
        }
        
        usedCards.Clear();
    }

    public void setTurnBuffer()
    {
        for (int i = handCards.Count - 1; i >= 0; i--)
        {
            Item temp = handCards[i];
            usedCards.Add(temp);
            handCards.Remove(temp);
        }

        for (int i= myCards.Count-1; i>=0; i--)
        {
            Card card = myCards[i];
            card.transform.DOKill();
            Destroy(card.gameObject);
            myCards.Remove(card);
        }
    }

    void SetupDeck()
    {
        
    }
    

    void OnDestroy()
    {
        DrawManager.onAddCard -= AddCard;
    }
    
    
    //카드 추가 함수
    void AddCard(bool check)
    {
        var nowItem = PopItem();
        handCards.Add(nowItem);
        var cardObject = Instantiate(nowItem.prefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.SetUp(nowItem);
        myCards.Add(card);
        SetOriginOrder();
        CardAlignment();
    }

    public void CardAlignment()
    {
        
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(LeftCard, RightCard,myCards.Count,2.0f,Vector3.one*1.0f);
        
        
        var targetCards = myCards;
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            
            targetCard.orginPRS=originCardPRSs[i];
            targetCard.MoveTransform(targetCard.orginPRS,true,0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>();

        switch (objCount)
        {
            case 1:
                objLerps = new float[] { 0.5f };
                break;
            case 2:
                objLerps = new float[] { 0.27f, 0.73f };
                break;
            case 3:
                objLerps = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            default:
                float interval = 1.0f / (objCount - 1);
                for (int i = 0; i < objCount; i++) objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Quaternion.identity;
            if (objCount>=4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i]*4 - 2.0f, 2));
                if (height < 0) curve *= -1;
                targetPos.y+=curve;
                targetRot=Quaternion.Slerp(rightTr.rotation, leftTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));

        }
        return results;
    }

    public bool TryUnitSpawn()
    {
        var card = selectedCard;
        var spawnPos = Utils.MousePos;
        spawnPos.y = 0;
        var targetCards= myCards;

        if (card.level > nowCost) return false;
        
        if (Utils.MousePos.y>1&&card.level <= nowCost)
        {
            AudioManager.Instance.PlaySFX(useCardSound);
            nowCost-=card.level;
            print(nowCost);
            UnitSpawnManager.Inst.SpawnUnit(spawnPos, card);
            targetCards.Remove(card);
            Item useItem = card.item;
            usedCards.Add(useItem);
            handCards.Remove(useItem);
            //card.transform.position = new Vector3(0, -40, 0);
            card.transform.DOKill();
            Destroy(card.gameObject);
            selectedCard = null;
            CardAlignment();
            return true;
        }
        foreach (var nowCard in targetCards)
        {
            nowCard.GetComponent<Order>().SetMostFrontOrder(false);
        }
        EnLargeCard(false, card);
        CardAlignment();

        return false;
        
    }

    public bool UsingUnitSkill()
    {
        var card = selectedCard;
        var targetCards = myCards;
        var spawnPos = Utils.MousePos;
        Vector2 mousePos = new Vector2(spawnPos.x, spawnPos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        
        if (hit.collider != null&&card.level <= nowCost)
        {
            var useUnit = hit.collider.gameObject.GetComponent<Unit>();
            if (useUnit != null && 
                useUnit.job_class == card.jobClass && 
                (useUnit.job_route == card.jobRoot || card.jobRoot == 0) && 
                useUnit.level >= card.level)
            {
                AudioManager.Instance.PlaySFX(useCardSound);
                nowCost-=card.level;
                print(nowCost);
                print("Skill");
                useUnit.skill = card.cardObj;
                useUnit.cast();
                targetCards.Remove(card);
                Item useItem = card.item;
                usedCards.Add(useItem);
                handCards.Remove(useItem);
                card.transform.DOKill();
                Destroy(card.gameObject);
                selectedCard = null;
                CardAlignment();
                return true;
            }
        }
        print("Skill use failed");
        foreach (var nowCard in targetCards)
        {
            nowCard.GetComponent<Order>().SetMostFrontOrder(false);
        }
        EnLargeCard(false, card);
        CardAlignment();

        return false;
    }

    
    public void SetOriginOrder()
    {
        int count=myCards.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        selectedCard = card;
        EnLargeCard(true,card);
    }
    public void CardMouseOut(Card card)
    {
        EnLargeCard(false,card);
    }
    
    public void CardMouseDown()
    {
        isCardDrawing = true;
    }

    public void CardMouseUp(bool mode)
    {
        isCardDrawing = false;
        if (mode) TryUnitSpawn();
        else UsingUnitSkill();
    }

    public void CardDrag()
    {
        if (!onMyCardArea)
        {
            //selectedCard.MoveTransform(new PRS(Utils.MousePos,Utils.QI,selectedCard.orginPRS.scale),false);
            EnLargeCard(true,selectedCard);
        }
    }

    public void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos,Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits,x=>x.collider.gameObject.layer==layer);
        
    }
    public void EnLargeCard(bool isLarge, Card card)
    {
        if (isLarge)
        {
            Vector3 enLargePos = new Vector3(card.orginPRS.pos.x, -6.0f, -10f);
            card.MoveTransform(new PRS(enLargePos, Utils.QI, Vector3.one*1.0f),false);
        }
        else
        {
            card.MoveTransform(card.orginPRS,false);
        }
        card.GetComponent<Order>().SetMostFrontOrder(isLarge);
       
    }
    
    #endregion
}
