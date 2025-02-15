using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override  void damaged(int damage)
    {
        HpBar hpBar = GetComponentInChildren<HpBar>();
        hp -= damage;
        anim.SetTrigger("3_Damaged");
        if (hp <= 0)
        {
            EnemySpawnManager.Inst.check = -1;
            anim.SetTrigger("4_Death");
            Destroy(gameObject);
        }
        hpBar.SetHealth(hp, maxhp);
        Debug.Log(hp);
    }
}
