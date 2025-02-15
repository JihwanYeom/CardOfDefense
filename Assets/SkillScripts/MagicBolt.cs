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
        // �簢�� ���� ���� ��� �浹ü ��������
        Collider2D[] objectsInRange = Physics2D.OverlapBoxAll(transform.position+pos, range, 0f, LayerMask.GetMask("Enemy"));

        // ã�� ������Ʈ�鿡 ���� �ݺ�
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