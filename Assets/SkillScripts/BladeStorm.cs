using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeStorm : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public Vector2 range;
    public Enemy target;

    public AudioClip skillSFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bladeStorm());
    }

    public IEnumerator bladeStorm()
    {
        AudioManager.Instance.PlaySFX(skillSFX);

        GameObject storm1 = Instantiate(effect1, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        storm1.GetComponent<SpriteRenderer>().flipY = true;
        yield return new WaitForSeconds(0.1f);

        GameObject storm2 = Instantiate(effect3, transform.position + new Vector3(-2, -0.5f, 0), transform.rotation);
        storm2.GetComponent<SpriteRenderer>().flipY = true;
        yield return new WaitForSeconds(0.1f);

        GameObject storm3 = Instantiate(effect2, transform.position + new Vector3(2, -0.5f, 0), transform.rotation);
        storm3.GetComponent<SpriteRenderer>().flipY = true;
        storm3.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.1f);

        GameObject storm4 = Instantiate(effect1, transform.position + new Vector3(-4, 0.5f, 0), transform.rotation);
        storm4.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.1f);

        GameObject storm5 = Instantiate(effect3, transform.position + new Vector3(4, 0.5f, 0), transform.rotation);
        
        

        Destroy(storm1, 3.0f);
        Destroy(storm2, 3.0f);
        Destroy(storm3, 3.0f);
        Destroy(storm4, 3.0f);
        Destroy(storm5, 3.0f);
        Destroy(gameObject, 3.5f);
        while (true)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, range, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                target = obj.GetComponent<Enemy>();
                target.damaged(damage);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
