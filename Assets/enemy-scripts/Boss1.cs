using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss1 : Enemy
{
    public GameObject attackPrefab;
    public int maxDistance;
    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        move();
        detect();
    }

    public override IEnumerator attack()
    {
        while (true)
        { 
            anim.SetTrigger("2_Attack");
            yield return new WaitForSeconds(1.0f);
            Vector2 attackPrefabPosition = new Vector2(transform.position.x, transform.position.y + 2.0f);
            GameObject slashAttack = Instantiate(attackPrefab, attackPrefabPosition, transform.rotation);
            slashAttack.GetComponent<SlashAttack>().effect = effect;
            slashAttack.GetComponent<SlashAttack>().fx = fx;
            slashAttack.GetComponent<SlashAttack>().atk = atk;
            slashAttack.GetComponent<SlashAttack>().maxDistance = maxDistance;
            slashAttack.GetComponent<SlashAttack>().layerMask = "Unit";
            yield return new WaitForSeconds(2.0f / attack_speed);
            
        }
    }
    
}

