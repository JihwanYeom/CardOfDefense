using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAuraEffect : MonoBehaviour
{
    List<GameObject> hitList;
    public RaycastHit2D hit;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        hitList = new List<GameObject>();
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 15.0f * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null && hitList.Contains(hit.collider.gameObject) == false)
        {
            GameObject target = hit.collider.gameObject;
            target.GetComponent<Enemy>().damaged(damage);
            target.GetComponent<Rigidbody2D>().AddForce(Vector2.right*5.0f);
            hitList.Add(hit.collider.gameObject);
        }
    }
}
