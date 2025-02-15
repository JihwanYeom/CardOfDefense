using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMage : Enemy
{
    public GameObject fireballprefab;
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
            anim.SetTrigger("2_Attack");
            yield return new WaitForSeconds(1.0f);
            GameObject fireball = Instantiate(fireballprefab, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);
            if (target != null)
            {
                fireball.GetComponent<Fireball>().damage = atk - target.def;
            }
            fireball.GetComponent<Fireball>().layerMask = "Unit";
            fireball.GetComponent<SpriteRenderer>().flipX = true;
            fireball.GetComponent<Fireball>().effect = effect;
            yield return new WaitForSeconds(2.0f / attack_speed);
        }
    }
}
