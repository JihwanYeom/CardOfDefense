using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArrow : MonoBehaviour
{
    public int damage;
    public float speed;
    public SpriteRenderer direction;
    public RaycastHit2D hit;
    public string layerMask;
    // Start is called before the first frame update
    void Start()
    {
        direction = GetComponent<SpriteRenderer>();
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction.flipX == true)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask(layerMask));
            if (hit.collider != null)
            {
                if(layerMask.Equals("Enemy"))
                    hit.collider.gameObject.GetComponent<Enemy>().damaged(damage);
                if (layerMask.Equals("Unit"))
                    hit.collider.gameObject.GetComponent<Unit>().damaged(damage);
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
                    hit.collider.gameObject.GetComponent<Enemy>().damaged(damage);
                if (layerMask.Equals("Unit"))
                    hit.collider.gameObject.GetComponent<Unit>().damaged(damage);
                Destroy(gameObject);
            }
        }
    }
}
