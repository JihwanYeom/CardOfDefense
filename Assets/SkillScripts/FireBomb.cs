using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Skill
{
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public AudioClip skillSFX1;
    public AudioClip skillSFX2;
    public Vector2 range; // 사각형 범위의 가로, 세로 크기
    public Enemy target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(firebomb());
    }

    public IEnumerator firebomb()
    {
        if (direction == -1)
        {
            effect1.GetComponent<SpriteRenderer>().flipY = true;
            effect1.GetComponent<FireBombBall>().speed = -10.0f;
        }
        if (direction == 1)
        {
            effect1.GetComponent<SpriteRenderer>().flipY = false;
            effect1.GetComponent<FireBombBall>().speed = 10.0f;
        }
        Quaternion bombRotation = Quaternion.Euler(0, 0, 90);
        GameObject bomb = Instantiate(effect1, transform.position + new Vector3(0, 0.5f, 0), bombRotation);
        

        
        AudioManager.Instance.PlaySFX(skillSFX1);
        yield return new WaitForSeconds(1.0f);

        GameObject explode = Instantiate(effect2, bomb.transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX2);
        Destroy(bomb);
        Destroy(explode, 0.5f);
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(explode.transform.position, range, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsInRange)
        {
            target = obj.GetComponent<Enemy>();
            target.damaged(damage * 2);
            GameObject particle = Instantiate(effect3, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
            Destroy(particle, 0.5f);
        }
        Destroy(gameObject, 2.0f);
    }
}
