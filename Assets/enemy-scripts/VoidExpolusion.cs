using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

public class VoidExpolusion : MonoBehaviour
{
    public int damage;
    public int atk;
    public float maxDistance;
    public SpriteRenderer direction;
    public float areaRadius;
    public GameObject voideffect;
    public GameObject voidfx;
    public GameObject expolusioneeffect;
    public GameObject expolusionfx;
    public Vector2 voidPosition;
    public Unit target;
    public Collider2D[] Unitlist;
    public int speed;
    
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 15.0f;
        areaRadius = 5.0f;
        Unitlist = null;
        StartCoroutine(skillAttack());
    }
    
    public void detect()
    {
        Vector2 rayPos = transform.position;
        rayPos.y = 0.5f;
        Debug.DrawRay(rayPos, Vector2.left * maxDistance, new Color(1,0,0));
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.left, maxDistance, LayerMask.GetMask("Unit"));

        if (target != null && target.gameObject == null)
            target = null;
        if (hit.collider != null)
            target = hit.collider.gameObject.GetComponent<Unit>();
        else
            target = null;
        if (target != null)
            distanceCal();
        else
            voidPosition = (Vector2)transform.position + Vector2.left * maxDistance;

    }

    public void distanceCal()
    {
        float x = 0;
        float y = 0;
        float cnt = 0;
        Unitlist = null;
        Unitlist =
            Physics2D.OverlapCircleAll(target.transform.position, areaRadius, LayerMask.GetMask("Unit"));
        foreach (Collider2D units in Unitlist)
        {
            x += units.transform.position.x;
            y += units.transform.position.y;
            cnt++;
        }
        voidPosition = new Vector2(x / cnt, 4.0f);
    }

    public void voidAttack()
    {
        voidfx = Instantiate(voideffect, voidPosition, voideffect.transform.rotation);
        Destroy(voidfx, 2.0f);
    }

    public void expolusionAttack()
    {
        expolusionfx = Instantiate(expolusioneeffect, voidPosition, expolusioneeffect.transform.rotation);
        Destroy(expolusionfx, 1.0f);
        foreach (Collider2D units in Unitlist)
        {
            Unit unitScript = units.GetComponent<Unit>();
            damage = atk - unitScript.def;
            if (damage < 0)
                damage = 0;
            unitScript.damaged(damage);
        }
    }

    public IEnumerator skillAttack()
    {
        while(true)
        {
            Debug.Log("!!");
            detect();
            if (Unitlist != null)
            {
                voidAttack();
                yield return new WaitForSeconds(2.0f);
                expolusionAttack();
                yield return new WaitForSeconds(5.0f);
            }
            else yield return new WaitForSeconds(4.0f);
        }
    }
    
}
