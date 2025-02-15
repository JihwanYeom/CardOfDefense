using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;
    public float speed;
    public SpriteRenderer direction;
    public RaycastHit2D hit;
    public string layerMask;
    public GameObject effect;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = GetComponent<SpriteRenderer>();
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.flipX == true)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask(layerMask));
            if (hit.collider != null)
            {
                if (layerMask.Equals("Enemy"))
                {
                    Enemy target = hit.collider.gameObject.GetComponent<Enemy>();
                    target.damaged(damage);
                    GameObject particle = Instantiate(effect, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
                    Destroy(particle, 0.5f);
                }

                if (layerMask.Equals("Unit"))
                {
                    Unit target = hit.collider.gameObject.GetComponent<Unit>();
                    target.damaged(damage);
                    GameObject particle = Instantiate(effect, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
                    Destroy(particle, 0.5f);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, LayerMask.GetMask(layerMask));
            if (hit.collider != null)
            {
                if (layerMask.Equals("Enemy"))
                {
                    Enemy target = hit.collider.gameObject.GetComponent<Enemy>();
                    target.damaged(damage);
                    GameObject particle = Instantiate(effect, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
                    Destroy(particle, 0.5f);
                }

                if (layerMask.Equals("Unit"))
                {
                    Unit target = hit.collider.gameObject.GetComponent<Unit>();
                    target.damaged(damage);
                    GameObject particle = Instantiate(effect, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);
                    Destroy(particle, 0.5f);
                }
                Destroy(gameObject);
            }
        }
    }
}
