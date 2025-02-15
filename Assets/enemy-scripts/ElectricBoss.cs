using System;
using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
//using UnityEditor.Tilemaps;
using UnityEngine;
//using UnityEngine.Jobs;

public class ElectricBoss : Enemy
{
    public GameObject Electric_effect;
    public GameObject Electric_fx;
    public float attackRadius = 5.0f;
    
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public override IEnumerator attack()
    {
        while (true)
        {
            anim.SetTrigger("2_Attack");
            //yield return new WaitForSeconds(1.0f);
            Electric_fx = Instantiate(Electric_effect, transform.position + new Vector3(-5.0f,2.0f,0), transform.rotation);
            Collider2D[] hitUnits =
                Physics2D.OverlapCircleAll(transform.position + new Vector3(-5.0f,2.0f,0), attackRadius, LayerMask.GetMask("Unit"));
            foreach (Collider2D unit in hitUnits)
            {
                Unit targetUnit = unit.GetComponent<Unit>();
                if (targetUnit != null)
                {
                    int damage = atk - targetUnit.def;
                    if (damage < 0)
                        damage = 0;
                    targetUnit.damaged(damage);
                }
            }

            Destroy(Electric_fx, 1.0f);
            yield return new WaitForSeconds(2.0f / attack_speed);
        }
    }
}