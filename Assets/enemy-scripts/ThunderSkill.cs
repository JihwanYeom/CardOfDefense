using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ThunderSkill : MonoBehaviour
{
    public int damage;
    public int atk;
    public float maxDistance;
    public float areaRadius;
    public GameObject Thundereffect;
    public GameObject Thunderfx;
    public Vector2 ThunderPosition;
    public Unit target;
    public Collider2D[] Unitlist;
    
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
            ThunderPosition = (Vector2)transform.position + Vector2.left * maxDistance + new Vector2(0, 9.0f);

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
        ThunderPosition = new Vector2(x / cnt, 9.0f);
    }

    public void ThunderAttack()
    {
        Thunderfx = Instantiate(Thundereffect, ThunderPosition, Thundereffect.transform.rotation);
        Destroy(Thunderfx, 1.0f);
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
            detect();
            if (Unitlist != null)
            {
                ThunderAttack();
                yield return new WaitForSeconds(5.0f);
            }
            else yield return new WaitForSeconds(4.0f);
        }
    }
    
}
