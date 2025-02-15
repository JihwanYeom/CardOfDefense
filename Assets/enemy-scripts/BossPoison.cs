using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoison : Enemy
{
    public GameObject attackPrefab;
    public GameObject skillPrefab;
    public int maxDistance;
    public GameObject poisoneffect;
    public GameObject poisonfx;
    void Start()
    {
        base.Start();
        StartCoroutine(skillAttack());
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
            yield return new WaitForSeconds(1.0f);
            Vector2 attackPrefabPosition = new Vector2(transform.position.x, transform.position.y + 2.0f);
            GameObject poisonBase = Instantiate(attackPrefab, attackPrefabPosition, transform.rotation);
            if (target != null)
                poisonBase.GetComponent<PoisonBaseAttack>().damage = atk - target.def;
            poisonBase.GetComponent<PoisonBaseAttack>().layerMask = "Unit";
            yield return new WaitForSeconds(2.0f / attack_speed);
        }
    }

    IEnumerator skillAttack()
    {
        while (true)
        {
            anim.SetTrigger("2_Attack");
            yield return new WaitForSeconds(1.0f);
            Vector2 skillPrefabPosition = new Vector2(transform.position.x, transform.position.y + 2.0f);
            GameObject slashAttack = Instantiate(skillPrefab, skillPrefabPosition, transform.rotation);
            slashAttack.GetComponent<PoisonSkill>().poisoneffect = poisoneffect;
            slashAttack.GetComponent<PoisonSkill>().poisonfx = poisonfx;
            slashAttack.GetComponent<PoisonSkill>().atk = atk;
            slashAttack.GetComponent<PoisonSkill>().maxDistance = maxDistance;
            slashAttack.GetComponent<PoisonSkill>().layerMask = "Unit";
            yield return new WaitForSeconds(7.0f / attack_speed);
        }
    }
}
