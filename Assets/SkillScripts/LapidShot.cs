using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapidShot : Skill
{
    public GameObject effect;
    public AudioClip skillSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lapidshot());
    }

    public IEnumerator lapidshot()
    {
        if (direction == -1)
        {
            effect.GetComponent<SpriteRenderer>().flipX = true;
            effect.GetComponent<LapidShotArrow>().speed = -10.0f;
        }
        if (direction == 1)
        {
            effect.GetComponent<SpriteRenderer>().flipX = false;
            effect.GetComponent<LapidShotArrow>().speed = 10.0f;
        }

        GameObject arrow1 = Instantiate(effect, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow1.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow1, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow2 = Instantiate(effect, transform.position + new Vector3(0, 0.7f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow2.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow2, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow3 = Instantiate(effect, transform.position + new Vector3(0, -0.3f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow3.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow3, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow4 = Instantiate(effect, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow4.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow4, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow5 = Instantiate(effect, transform.position + new Vector3(0, -0.7f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow5.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow5, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow6 = Instantiate(effect, transform.position + new Vector3(0, 0.3f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow6.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow6, 3.0f);
        yield return new WaitForSeconds(0.1f);

        GameObject arrow7 = Instantiate(effect, transform.position + new Vector3(0, 0, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        arrow7.GetComponent<LapidShotArrow>().damage = (int)(damage * 0.7);
        Destroy(arrow7, 3.0f);
        Destroy(gameObject);
    }
    
}


