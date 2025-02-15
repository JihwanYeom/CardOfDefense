using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoEffect : MonoBehaviour
{
    public int damage;
    void Start()
    {
        StartCoroutine(fireTornadoEffect());
    }

    void Update()
    {
        transform.position += Vector3.right * 10.0f * Time.deltaTime;
    }

    public IEnumerator fireTornadoEffect()
    {
        while (true)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                Enemy target = obj.GetComponent<Enemy>();
                target.damaged(damage);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
