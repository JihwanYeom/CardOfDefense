using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public bool cardType;
    public GameObject cardObj;
    public Item item;
    public PRS orginPRS;
    public int jobClass;
    public int jobRoot;
    public int level;

    public void SetUp(Item item)
    {
        this.item = item;

        //character.sprite = this.item.sprite;
        //nameTMP.text = this.item.name;
    }

    public void OnMouseOver()
    {
        if(Time.timeScale == 1f)
            CardManager.Inst.CardMouseOver(this);
    }

    public void OnMouseExit()
    {
        if(Time.timeScale == 1f)
            CardManager.Inst.CardMouseOut(this);
    }

    public void OnMouseDown()
    {
        if(Time.timeScale == 1f)
            CardManager.Inst.CardMouseDown();
    }

    public void OnMouseUp()
    {
        if(Time.timeScale == 1f)
            CardManager.Inst.CardMouseUp(cardType);
    }

    public void MoveTransform(PRS prs, bool useDoTween, float dotweenTime = 0)
    {
        if (useDoTween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
