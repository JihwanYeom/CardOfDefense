using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit
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
            GameObject arrow = Instantiate(effect, transform.position + new Vector3(2.0f, 1.0f, 0), transform.rotation);
            if (target != null)
                arrow.GetComponent<Arrow>().damage = atk - target.def;
            arrow.GetComponent<Arrow>().layerMask = "Enemy";
            if (direction == Direction.LEFT)
                arrow.GetComponent<SpriteRenderer>().flipX = true;
            else
                arrow.GetComponent<SpriteRenderer>().flipX = false;
            yield return new WaitForSeconds(1.0f / attack_speed);
        }
    }
    

}
