using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanctuary : Skill
{
    public Vector2 range;
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;

    public AudioClip skillSFX;

    void Start()
    {
        StartCoroutine(sanctuary());
    }

    public IEnumerator sanctuary()
    {
        AudioManager.Instance.PlaySFX(skillSFX);
        GameObject cross = Instantiate(effect1, transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
        GameObject shield = Instantiate(effect2, transform.position + new Vector3(0, 4.0f, 0), Quaternion.identity);
        GameObject circle = Instantiate(effect3, transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);

        for (int i = 0; i < 1000; i++)
        {
            Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(shield.transform.position, range, 0f, LayerMask.GetMask("Unit"));

            foreach (Collider2D obj in objectsInRange)
            {
                Unit unit = obj.GetComponent<Unit>();
                unit.hp = unit.maxhp;
            }

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(cross);
        Destroy(shield);
        Destroy(circle);
        Destroy(gameObject);
    }
}
