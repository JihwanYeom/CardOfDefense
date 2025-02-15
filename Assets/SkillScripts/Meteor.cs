using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;
    public Vector2 range; // 사각형 범위의 가로, 세로 크기
    public Enemy target;

    public AudioClip skillSFX1;
    public AudioClip skillSFX2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(meteor());
    }

    public IEnumerator meteor()
    {
        GameObject bomb = Instantiate(effect1, transform.position + new Vector3(0, 14.0f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX1);
        for (int i = 0; i < 145; i++)
        {
            bomb.transform.localScale = new Vector3((float)(13* ((float)i/100)), (float)(10 * ((float)i / 100)),0);
            bomb.transform.position = bomb.transform.position + new Vector3(0.05f, -0.1f, 0);
            yield return new WaitForSeconds(0.005f);
        }

        AudioManager.Instance.PlaySFX(skillSFX2);
        GameObject explode = Instantiate(effect2, bomb.transform.position + new Vector3(0, 7.0f, 0), transform.rotation);
        GameObject flame1 = Instantiate(effect3, bomb.transform.position + new Vector3(8.0f, 6.0f, 0), transform.rotation);
        GameObject flame2 = Instantiate(effect4, bomb.transform.position + new Vector3(-8.0f, 6.0f, 0), transform.rotation);
        Destroy(bomb);
        Destroy(explode, 0.5f);
        Destroy(flame1, 0.8f);
        Destroy(flame2, 0.8f);
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(explode.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage * 2);
        }
        
        for (int i = 0; i < 2; i++)
        {
            objectsInRange = Physics2D.OverlapBoxAll(transform.position, range * 2, 0f, LayerMask.GetMask("Enemy"));
            foreach (Collider2D obj in objectsInRange)
            {
                target = obj.GetComponent<Enemy>();
                target.damaged(damage * 2);
            }
            yield return new WaitForSeconds(0.4f);
        }
        Destroy(gameObject, 5.0f);
    }
}
