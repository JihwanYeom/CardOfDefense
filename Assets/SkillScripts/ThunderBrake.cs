using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBrake : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;
    public GameObject effect5;
    public GameObject effect6;
    public Vector2 range;
    public Enemy target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bladeStorm());
    }

    public IEnumerator bladeStorm()
    {
        GameObject storm1 = Instantiate(effect1, transform.position + new Vector3(0, 5.0f, 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);

        GameObject storm2 = Instantiate(effect2, transform.position + new Vector3(-4, 5.0f, 0), transform.rotation);
        yield return new WaitForSeconds(0.3f);

        GameObject storm3 = Instantiate(effect3, transform.position + new Vector3(4, 5.0f, 0), transform.rotation);
        yield return new WaitForSeconds(0.2f);

        GameObject storm4 = Instantiate(effect4, transform.position + new Vector3(-8, 5.0f, 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);

        GameObject storm5 = Instantiate(effect5, transform.position + new Vector3(-8, 5.0f, 0), transform.rotation);
        yield return new WaitForSeconds(0.2f);

        GameObject storm6 = Instantiate(effect6, transform.position + new Vector3(8, 5.0f, 0), transform.rotation);



        Destroy(storm1, 10.0f);
        Destroy(storm2, 10.0f);
        Destroy(storm3, 10.0f);
        Destroy(storm4, 10.0f);
        Destroy(storm5, 10.0f);
        Destroy(storm6, 10.0f);
        Destroy(gameObject, 10.5f);
        while (true)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, range, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                target = obj.GetComponent<Enemy>();
                target.damaged(damage);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
