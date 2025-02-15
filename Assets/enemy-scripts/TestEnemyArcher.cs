using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyArcher : Enemy
{
    public GameObject arrowPrefab;

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
            GameObject arrow = Instantiate(arrowPrefab, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);
            if (target != null)
            {
                arrow.GetComponent<Arrow>().damage = atk - target.def;
            }
            arrow.GetComponent<Arrow>().layerMask = "Unit";
            arrow.GetComponent<SpriteRenderer>().flipX = true;
            arrow.GetComponent<Arrow>().effect = effect;
            yield return new WaitForSeconds(2.0f/attack_speed);
            
        }
    }
}
