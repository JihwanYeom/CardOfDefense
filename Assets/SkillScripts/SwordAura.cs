using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;

    public AudioClip skillSFX;


    void Start()
    {
        StartCoroutine(swordAura());
    }

    public IEnumerator swordAura()
    {
        AudioManager.Instance.PlaySFX(skillSFX);

        GameObject slash = Instantiate(effect1, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        Destroy(slash, 0.5f);
        yield return new WaitForSeconds(0.1f);
        Quaternion auraRotation = Quaternion.Euler(0, 0, 20);
        GameObject aura1 = Instantiate(effect2, transform.position + new Vector3(0, 0.5f, 0), auraRotation);
        GameObject aura2 = Instantiate(effect3, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);
    }
}
