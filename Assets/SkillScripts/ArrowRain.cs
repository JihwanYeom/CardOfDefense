using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : Skill
{
    public GameObject effect;
    public AudioClip skillSFX;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(arrowRain());
    }

    public IEnumerator arrowRain()
    {
        for(int i = 0; i < 100; i++)
        {
            GameObject arrow = Instantiate(effect, transform.position, transform.rotation);
            if(i%5 == 0)
                AudioManager.Instance.PlaySFX(skillSFX);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
