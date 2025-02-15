using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainEffect : MonoBehaviour
{
    public float launchForce = 20.0f;
    public GameObject effect;
    public Vector2 direction;
    private Rigidbody2D rb;
    public RaycastHit2D hit;
    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(2.0f + (float)Random.Range(0, 10) * 0.1f, 1.0f + (float)Random.Range(0, 50) * 0.1f);
        LaunchArrow();
    }

    void LaunchArrow()
    {
        rb.velocity = direction.normalized * launchForce * 2.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if(rb.velocity.y < 0.5f)
        {
            rb.gravityScale = 20;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy hit = collision.gameObject.GetComponent<Enemy>();
        if(hit != null)
        {
            hit.damaged(damage);
        }
        GameObject particle = Instantiate(effect, transform.position + new Vector3(0, -1.0f, 0), transform.rotation);
        Destroy(particle, 0.5f);
        Destroy(gameObject);
    }
}
