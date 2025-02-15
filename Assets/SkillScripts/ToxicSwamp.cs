using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSwamp : Skill
{
    public Vector2 range;
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;
    List<Enemy> enemyInEffect;

    public AudioClip skillSFX1;
    public AudioClip skillSFX2;

    void Start()
    {
        enemyInEffect = new List<Enemy>();
        StartCoroutine(toxicSwamp());
    }

    public IEnumerator toxicSwamp()
    {
        GameObject arrow = Instantiate(effect2, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        AudioManager.Instance.PlaySFX(skillSFX1);
        for (int i = 0; i < 150; i++)
        {
            arrow.transform.position = arrow.transform.position + new Vector3(0.1f, 0, 0);
            yield return new WaitForSeconds(0.005f);
        }
        
        GameObject swamp = Instantiate(effect1, arrow.transform.position + new Vector3(0, 4.0f, 0), Quaternion.identity);
        GameObject gas = Instantiate(effect4, arrow.transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
        Destroy(arrow);
        AudioManager.Instance.PlaySFX(skillSFX2);
        for (int i = 0; i < 100; i++)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(swamp.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                Enemy target = obj.GetComponent<Enemy>();
                if (enemyInEffect.Contains(target))
                    continue;
                Transform targetTransform = target.transform;
                GameObject debuffEffect = Instantiate(effect3, targetTransform.position, Quaternion.identity);
                debuffEffect.transform.SetParent(targetTransform);
                debuffEffect.transform.position = targetTransform.position + new Vector3(0, 1.0f, 0);

                target.move_speed = target.move_speed / 5;
                enemyInEffect.Add(target);

                
            }
            if (i % 5 == 0)
            {
                foreach (Enemy effected in enemyInEffect)
                {
                    if(effected != null)
                        effected.damaged(10);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        foreach (Enemy target in enemyInEffect)
        {
            if (target != null)
            {
                target.move_speed = target.move_speed * 5;
                Transform buffTransform = target.transform.Find("ToxicSwamp3(Clone)");
                Destroy(buffTransform.gameObject);
            }
        }
        Destroy(swamp);
        Destroy(gas);
        Destroy(gameObject);
    }
}