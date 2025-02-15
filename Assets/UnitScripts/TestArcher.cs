using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArcher : Unit
{
    // Start is called before the first frame update
    public GameObject arrowPrefab;
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
            GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
            if (target != null)
                arrow.GetComponent<TestArrow>().damage = atk - target.def;
            arrow.GetComponent<TestArrow>().layerMask = "Enemy";
            //arrow.GetComponent<SpriteRenderer>().flipX = direction.flipX;
            yield return new WaitForSeconds(1.0f / attack_speed);
        }
    }
}
