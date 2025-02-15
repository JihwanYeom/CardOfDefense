using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : Skill
{
    public Vector2 range;
    public GameObject effect;
    public Enemy target;

    void Start()
    {
        // 사각형 범위 안의 모든 충돌체 가져오기
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position+pos, range, 0f, LayerMask.GetMask("Enemy"));

        // 찾은 오브젝트들에 대해 반복
        foreach (Collider2D obj in objectsInRange)
        {
            GameObject thunder = Instantiate(effect, obj.transform.position + new Vector3(0, 2.0f, 0), obj.transform.rotation);
            Destroy(thunder, 0.5f);
            target = obj.GetComponent<Enemy>();
            target.damaged(damage * 2);
        }
        Destroy(gameObject, 2.0f);
    }
}