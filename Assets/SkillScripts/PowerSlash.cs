using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlash : Skill
{
    public Vector2 range;
    public GameObject effect1;
    public GameObject effect2;
    public AudioClip skillSFX;
    public Enemy target;

    void Start()
    {
        StartCoroutine(powerSlash());
    }

    public IEnumerator powerSlash()
    {
        // 사각형 범위 안의 모든 충돌체 가져오기
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        if(direction == -1)
        {
            effect1.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (direction == 1)
        {
            effect1.GetComponent<SpriteRenderer>().flipX = true;
        }
        GameObject slash = Instantiate(effect1, transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        Destroy(slash, 0.5f);
        yield return new WaitForSeconds(0.2f);

        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage*2 - target.def);
            GameObject particle = Instantiate(effect2, target.transform.position + new Vector3(0,1.0f,0), target.transform.rotation);
            Destroy(particle, 0.5f);
        }
        
        Destroy(gameObject, 2.0f);
    }
}
