using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
//using UnityEditor.PackageManager;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    public float speed;
    public int damage;
    public int atk;
    public int maxDistance;
    public SpriteRenderer direction;
    public RaycastHit2D hit;
    public string layerMask;
    public GameObject effect;
    public GameObject fx;

    public Vector2 startPosition;
    
    public void Start()
    {
        speed = 30.0f;
        startPosition = transform.position;
        direction = GetComponent<SpriteRenderer>();
        maxDistance = 15;
    }

    public void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask(layerMask));
        if (hit.collider != null)
        {
            if (layerMask.Equals("Unit"))
            {
                fx = Instantiate(effect, hit.transform.position + new Vector3(-0.8f, 2.0f, 0), hit.transform.rotation);
                damage = atk - hit.collider.gameObject.GetComponent<Unit>().def;
                hit.collider.gameObject.GetComponent<Unit>().damaged(damage);
                transform.position += Vector3.left * 0.1f;
                Destroy(fx,1.0f);
            }
        }
        if(Vector2.Distance(startPosition, transform.position) >= maxDistance)
            Destroy(gameObject);
    }
}