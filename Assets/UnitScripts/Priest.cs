using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Mage
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

            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, new Vector2(range*2,range*2), 0f, LayerMask.GetMask("Unit"));
            List<Unit> unitInRange = new List<Unit> { };
            foreach (Collider2D obj in objectsInRange)
            {
                if (obj.GetComponent<Unit>() == this)
                    continue;
                unitInRange.Add(obj.GetComponent<Unit>());
            }
            if (unitInRange.Count > 0)
            {
                unitInRange.Sort((a, b) => a.hp.CompareTo(b.hp));
                GameObject heal = Instantiate(effect, unitInRange[0].transform.position + new Vector3(0.0f, 1.0f, 0.0f), transform.rotation);
                Destroy(heal, 0.5f);
                unitInRange[0].hp += atk;
                if (unitInRange[0].hp > unitInRange[0].maxhp)
                    unitInRange[0].hp = unitInRange[0].maxhp;
                unitInRange[0].damaged(0);
            }
            yield return new WaitForSeconds(1.0f / attack_speed);
        }
    }
}
