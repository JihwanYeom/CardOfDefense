using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMage1 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override IEnumerator attack()
    {
        while (true)
        {
            if (target != null)
            {
                anim.SetTrigger("2_Attack");
                yield return new WaitForSeconds(0.5f);
                fx = Instantiate(effect, target.transform.position + new Vector3(-0.8f, 2.0f, 0),
                    target.transform.rotation);
                int damage = atk - target.def;
                if (damage < 0)
                    damage = 0;
                target.damaged(damage);
                Destroy(fx, 1.0f);
                yield return new WaitForSeconds(2.0f / attack_speed);
            }
            else
                yield break;
        }
    }
}
