using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapidShotArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float speed;
    public RaycastHit2D hit;
    public GameObject effect;
    // Update is called once per frame

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (speed < 0)
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask("Enemy"));
        else
            hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
        {
            Enemy target = hit.collider.gameObject.GetComponent<Enemy>();
            if(target.hp > 0)
            {
                target.damaged(damage - target.def);
                GameObject particle;
                if (speed < 0)
                    particle = Instantiate(effect, target.transform.position + new Vector3(0, -1.0f, 0), target.transform.rotation);
                else
                    particle = Instantiate(effect, target.transform.position + new Vector3(0, 1.0f, 0), target.transform.rotation);

                Destroy(particle, 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
