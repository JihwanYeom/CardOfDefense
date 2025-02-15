using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBaseAttack : MonoBehaviour
{
    public float speed;
    public int damage;
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
                fx = Instantiate(effect, hit.transform.position + new Vector3(-0.8f, 1.0f, 0), hit.transform.rotation);
                hit.collider.gameObject.GetComponent<Unit>().damaged(damage);
                Destroy(fx, 1.0f);
            }
            Destroy(gameObject);
        }
    }
}