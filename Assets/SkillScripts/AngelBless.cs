using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBless : Skill
{
    public Vector2 range;
    public GameObject effect;
    public Unit target;
    public AudioClip skillSFX;

    void Start()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, range, 0f, LayerMask.GetMask("Unit"));
        AudioManager.Instance.PlaySFX(skillSFX);
        foreach (Collider2D obj in objectsInRange)
        {
            GameObject wing = Instantiate(effect, obj.transform.position + new Vector3(0, 2.0f, 0), obj.transform.rotation);
            Destroy(wing, 0.5f);
            target = obj.GetComponent<Unit>();
            target.hp += damage;
            if (target.hp > target.maxhp)
                target.hp = target.maxhp;
            target.damaged(0);
        }
    }
}
