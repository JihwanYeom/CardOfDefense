using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultOrder : Skill
{
    public Vector2 range;
    public GameObject effect;
    List<Unit> unitInEffect;

    public AudioClip skillSFX;

    void Start()
    {
        unitInEffect = new List<Unit>();
        StartCoroutine(assaultOrder());
    }

    public IEnumerator assaultOrder()
    {
        AudioManager.Instance.PlaySFX(skillSFX);
        for (int i = 0; i < 100; i++)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, range, 0f, LayerMask.GetMask("Unit"));
            foreach (Collider2D obj in objectsInRange)
            {
                
                Unit unit = obj.GetComponent<Unit>();
                if (unitInEffect.Contains(unit))
                    continue;

                Transform unitTransform = unit.transform;
                GameObject buffEffect = Instantiate(effect, unitTransform.position, Quaternion.identity);
                buffEffect.transform.SetParent(unitTransform);
                buffEffect.transform.position = unitTransform.position + new Vector3(0,2.0f,0);

                unit.atk = unit.atk * 2;
                unit.move_speed = unit.move_speed * 4;
                unit.attack_speed = unit.attack_speed * 3;
                unit.vision = unit.vision * 10;
                unitInEffect.Add(unit);
            }
            yield return new WaitForSeconds(0.1f);
        }

        foreach (Unit unit in unitInEffect)
        {
            unit.atk = unit.atk / 2;
            unit.move_speed = unit.move_speed / 4;
            unit.attack_speed = unit.attack_speed / 3;
            unit.vision = unit.vision / 10;

            Transform buffTransform = unit.transform.Find("AssaultOrder2(Clone)"); ;
            Destroy(buffTransform.gameObject);
        }
        Destroy(gameObject);

    }
}
