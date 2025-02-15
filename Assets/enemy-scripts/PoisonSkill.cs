using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PoisonSkill : MonoBehaviour
{
    public int damage;
    public int atk;
    public float maxDistance;
    public float areaRadius;
    public GameObject poisoneffect;
    public GameObject poisonfx;
    public float speed;
    public string layerMask;
    public RaycastHit2D hit;

    public Vector2 startPosition;
    void Start()
    {
        speed = 30.0f;
        startPosition = transform.position;
        maxDistance = 15;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask(layerMask));
        if (layerMask.Equals("Unit") && hit.collider != null)
        {
            StartCoroutine(poisonDamage(hit.collider.gameObject));
        }
        if(Vector2.Distance(startPosition, transform.position) >= maxDistance)
            Destroy(gameObject);
    }

    IEnumerator poisonDamage(GameObject target)
    {
        for (int i = 0; i < 3; i++)
        {
            if (target != null)
            {
                poisonfx = Instantiate(poisoneffect, target.transform.position + new Vector3(-0.8f, 2.0f, 0), target.transform.rotation);
                damage = atk - target.GetComponent<Unit>().def;
                target.GetComponent<Unit>().damaged(damage);
                Destroy(poisonfx,1.0f);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
