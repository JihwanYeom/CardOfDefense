using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public Vector2 range;

    public AudioClip skillSFX;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(earthquake());
    }

    public IEnumerator earthquake()
    {
        for(int i = 0; i < 3; i++)
        {
            AudioManager.Instance.PlaySFX(skillSFX);
            GameObject slash = Instantiate(effect1, transform.position, transform.rotation);
            GameObject quake = Instantiate(effect2, transform.position + new Vector3(15.0f, 2.0f, 0), transform.rotation);
            Destroy(slash, 0.5f);
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(quake.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
            Destroy(quake, 0.5f);
            foreach (Collider2D obj in objectsInRange)
            {
                Enemy target = obj.GetComponent<Enemy>();
                target.transform.position =  new Vector3(target.transform.position.x, 5.0f, 0);
                target.damaged(damage);
                GameObject particle = Instantiate(effect3, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
                Destroy(particle, 0.5f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(gameObject);
    }
}
