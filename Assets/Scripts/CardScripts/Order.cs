using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] midRenderers;
    [SerializeField] string sortingLayerName;
    public int originOrder;
    public Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetOriginOrder(int order)
    {
        this.originOrder = order;
        renderer.sortingOrder = this.originOrder;
        SetOrder(order);
    }
    
    public void SetMostFrontOrder(bool isFront)
    {
        if (isFront) renderer.sortingOrder = originOrder + 1000;
        else renderer.sortingOrder= originOrder;
    }

    public void SetOrder(int order)
    {
        int mulOrder = order*10;
        
        foreach(var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
            
        }

        foreach (var renderer in midRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder+1;
        }
    }
}
