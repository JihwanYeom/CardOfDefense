using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public SpriteRenderer hpBar; // 체력바 SpriteRenderer
    public SpriteRenderer hpBarBackground; // 체력바 SpriteRenderer
    public SpriteMask hpBarMask; // 체력바 Sprite Mask

    private static int orderCounter = 0; // 유닛마다 고유 Order 값을 부여하기 위한 정적 변수
    private int myOrder; // 현재 유닛의 Order 값

    public void Start()
    {
        myOrder = orderCounter++;
        Transform hpTransform = transform.Find("health_bar");
        Transform hpBackTransform = transform.Find("empty_bar");

        hpBar = hpTransform.GetComponent<SpriteRenderer>(); // 체력바 SpriteRenderer
        hpBarBackground = hpBackTransform.GetComponent<SpriteRenderer>(); // 체력바 SpriteRenderer
        hpBarMask = GetComponentInChildren<SpriteMask>(); // 체력바 Sprite Mask

        // SpriteRenderer와 SpriteMask의 Order 설정
        hpBar.sortingOrder = myOrder;
        hpBarBackground.sortingOrder = myOrder - 1;
        hpBarMask.frontSortingOrder = myOrder;
        hpBarMask.backSortingOrder = myOrder - 1; // Sprite Mask 영역 유지
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float hpRatio = currentHealth / maxHealth;
        hpBarMask.transform.localScale = new Vector3(hpRatio, 0.2f, 1);
    }
}
