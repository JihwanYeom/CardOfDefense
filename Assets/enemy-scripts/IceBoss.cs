using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;
public class IceBoss : Enemy
{
    public GameObject attackPrefab;
    public GameObject ice_effect;
    public GameObject ice_fx;
    public GameObject skillPrefab;
    public int maxDistance;
    
    void Start()
    {
        base.Start();
        maxDistance = 15;
        StartCoroutine(IceSkill());
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
            GameObject iceBase = Instantiate(attackPrefab, attackPrefabPosition, transform.rotation);
            if (target != null)
                iceBase.GetComponent<IceBaseAttack>().damage = atk - target.def;
            iceBase.GetComponent<IceBaseAttack>().layerMask = "Unit";
            yield return new WaitForSeconds(2.0f / attack_speed);
        }
    }

    public IEnumerator IceSkill()
    {
        while (true)
        {
            anim.SetTrigger("2_Attack");
            yield return new WaitForSeconds(1.0f);
            Vector2 skillPreFabPosition = new Vector2(transform.position.x, transform.position.y + 2.0f);
            GameObject iceSkillEffect = Instantiate(skillPrefab, skillPreFabPosition, transform.rotation);
            iceSkillEffect.GetComponent<IceSkill>().effect = effect;
            iceSkillEffect.GetComponent<IceSkill>().atk = atk;
            iceSkillEffect.GetComponent<IceSkill>().maxDistance = maxDistance;
            iceSkillEffect.GetComponent<IceSkill>().layerMask = "Unit";
            yield return new WaitForSeconds(5.0f / attack_speed);

        }
    }
    
}