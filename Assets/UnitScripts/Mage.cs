using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
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
            anim.SetTrigger("2_Attack");
            yield return new WaitForSeconds(0.5f);
            AudioManager.Instance.PlaySFX(attackSFX);
            GameObject fireball = Instantiate(effect, transform.position + new Vector3(2.0f, 1.0f, 0), transform.rotation);
            if (target != null)
                fireball.GetComponent<Fireball>().damage = atk - target.def;
            fireball.GetComponent<Fireball>().layerMask = "Enemy";
            if (direction == Direction.LEFT)
                fireball.GetComponent<SpriteRenderer>().flipX = true;
            else
                fireball.GetComponent<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(1.0f / attack_speed);
        }
    }

}