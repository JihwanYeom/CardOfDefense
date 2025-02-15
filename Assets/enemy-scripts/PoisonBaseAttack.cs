using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBaseAttack : MonoBehaviour
{
    public float speed;
    public int damage;
    public int atk;
    public RaycastHit2D hit;
    public string layerMask;
    public GameObject effect;
    public GameObject fx;
    
    // Start is called before the first frame update
    public void Start()
    {
        speed = 20.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask(layerMask));
        if (hit.collider != null)
        {
            if (layerMask.Equals("Unit"))
            {
                Destroy(gameObject);
                StartCoroutine(poisonDamage(hit.collider.gameObject));
            }
        }
    }

    IEnumerator poisonDamage(GameObject target)
    {
        for (int i = 0; i < 3; i++)
        {
            if (target != null)
            {
                fx = Instantiate(effect, target.transform.position + new Vector3(-0.8f, 2.0f, 0), target.transform.rotation);
                target.GetComponent<Unit>().damaged(damage);
                Destroy(fx, 0.5f);  
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
