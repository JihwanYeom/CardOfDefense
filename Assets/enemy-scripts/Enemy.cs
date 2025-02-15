using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public int hp;
    public int maxhp;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public float attack_speed;
    public float move_speed;
    public float range;
    public bool inBattle;
    public Unit target;
    public Animator anim;
    public GameObject effect;
    public GameObject fx;

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    public void Update()
    {
        detect();
        move();
    }

    public void move()
    {
        if (!inBattle)
        {
            transform.position += Vector3.left * move_speed * Time.deltaTime;
            anim.SetBool("1_Move", true);
        }
    }

    private Coroutine attackCoroutine;
    public void detect()
    {
        Vector3 rayPos = transform.position;
        rayPos.y = 0.5f;
        //Debug.DrawRay(rayPos, Vector2.left * range, new Color(1, 0, 0));
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.left, range, LayerMask.GetMask("Unit"));
        
        if (hit.collider != null)
        {
            target =  hit.collider.gameObject.GetComponent<Unit>();
            if (target == null)
            {
                target = null;
            }
        }
        else
            target = null; 

        if (target != null)
        {
            if (!inBattle)
            {
                anim.SetBool("1_Move", false);
                attackCoroutine = StartCoroutine(attack());
            }
            inBattle = true;
        }
        else if (inBattle)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            inBattle = false;
        }
    }

    public void damaged(int damage)
    {
        HpBar hpBar = GetComponentInChildren<HpBar>();
        hp -= damage;
        anim.SetTrigger("3_Damaged");
        if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("4_Death");
            move_speed = 0;
            Destroy(gameObject, 0.8f);
            Destroy(fx);
        }
        hpBar.SetHealth(hp, maxhp);
    }

    public virtual IEnumerator attack()
    {
        while (true)
        {
            if (target != null)
            {
                fx = Instantiate(effect, transform.position + new Vector3(-0.8f, 0.5f, 0), transform.rotation);
                Destroy(fx, 0.8f);
                anim.SetTrigger("2_Attack");
                int damage = atk - target.def;
                if (damage < 0)
                    damage = 0;
                target.damaged(damage);             
                yield return new WaitForSeconds(2.0f / attack_speed);
            }
            else
                yield break;
        }
    }
}
