using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Skill
{
    public GameObject effect;
    public Vector2 range;
    public Enemy target;
    Collider2D[] objectsInRange;

    void Start()
    {
        StartCoroutine(iceSpike());
    }

    public IEnumerator iceSpike()
    {
        GameObject ice1 = Instantiate(effect, transform.position + new Vector3(2.0f, 0.5f, 0), transform.rotation);
        Destroy(ice1, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice1.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice2 = Instantiate(effect, transform.position + new Vector3(4.0f, 0.5f, 0), transform.rotation);
        Destroy(ice2, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice2.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice3 = Instantiate(effect, transform.position + new Vector3(6.0f, 0.5f, 0), transform.rotation);
        Destroy(ice3, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice3.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice4 = Instantiate(effect, transform.position + new Vector3(8.0f, 0.5f, 0), transform.rotation);
        Destroy(ice4, 0.5f);
        objectsInRange = Physics2D.OverlapBoxAll(ice4.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage);
        }
        yield return new WaitForSeconds(0.2f);

        GameObject ice5 = Instantiate(effect, transform.position + new Vector3(10.0f, 0.5f, 0), transform.rotation);
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
