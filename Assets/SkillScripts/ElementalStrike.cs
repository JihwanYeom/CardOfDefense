using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalStrike : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public Vector2 range;
    public Enemy target;
    Collider2D[] objectsInRange;

    public AudioClip skillSFX1;
    public AudioClip skillSFX2;
    public AudioClip skillSFX3;


    void Start()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            AudioManager.Instance.PlaySFX(skillSFX1);
            GameObject tornado = Instantiate(effect1, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);
            Destroy(tornado, 5.0f);
        }
        else if(rand == 1)
        {
            AudioManager.Instance.PlaySFX(skillSFX2);
            StartCoroutine(iceSpike());
        }
        else if(rand == 2)
        {
            AudioManager.Instance.PlaySFX(skillSFX3);
            range = new Vector2(20, 5);
            objectsInRange = Physics2D.OverlapBoxAll(transform.position + pos, range, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                GameObject thunder = Instantiate(effect3, obj.transform.position + new Vector3(0, 2.0f, 0), obj.transform.rotation);
                Destroy(thunder, 0.5f);
                target = obj.GetComponent<Enemy>();
                target.damaged(damage * 2);
            }
            Destroy(gameObject, 2.0f);
        }
    }

    public IEnumerator iceSpike()
    {
        range = new Vector2(5, 5);
        GameObject ice1 = Instantiate(effect2, transform.position + new Vector3(2.0f, 0.5f, 0), transform.rotation);
        Destroy(ice1, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice1.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice2 = Instantiate(effect2, transform.position + new Vector3(4.0f, 0.5f, 0), transform.rotation);
        Destroy(ice2, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice2.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice3 = Instantiate(effect2, transform.position + new Vector3(6.0f, 0.5f, 0), transform.rotation);
        Destroy(ice3, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice3.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice4 = Instantiate(effect2, transform.position + new Vector3(8.0f, 0.5f, 0), transform.rotation);
        Destroy(ice4, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice4.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice5 = Instantiate(effect2, transform.position + new Vector3(10.0f, 0.5f, 0), transform.rotation);
        Destroy(ice5, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice5.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        Destroy(gameObject);
    }

}
