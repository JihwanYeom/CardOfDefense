using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public SpriteRenderer hpBar; // ü�¹� SpriteRenderer
    public SpriteRenderer hpBarBackground; // ü�¹� SpriteRenderer
    public SpriteMask hpBarMask; // ü�¹� Sprite Mask

    private static int orderCounter = 0; // ���ָ��� ���� Order ���� �ο��ϱ� ���� ���� ����
    private int myOrder; // ���� ������ Order ��

    public void Start()
    {
        myOrder = orderCounter++;
        Transform hpTransform = transform.Find("health_bar");
        Transform hpBackTransform = transform.Find("empty_bar");

        hpBar = hpTransform.GetComponent<SpriteRenderer>(); // ü�¹� SpriteRenderer
        hpBarBackground = hpBackTransform.GetComponent<SpriteRenderer>(); // ü�¹� SpriteRenderer
        hpBarMask = GetComponentInChildren<SpriteMask>(); // ü�¹� Sprite Mask

        // SpriteRenderer�� SpriteMask�� Order ����
        hpBar.sortingOrder = myOrder;
        hpBarBackground.sortingOrder = myOrder - 1;
        hpBarMask.frontSortingOrder = myOrder;
        hpBarMask.backSortingOrder = myOrder - 1; // Sprite Mask ���� ����
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float hpRatio = currentHealth / maxHealth;
        hpBarMask.transform.localScale = new Vector3(hpRatio, 0.2f, 1);
    }
}
