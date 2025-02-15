using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : Skill
{
    List<GameObject> hitList;
    public RaycastHit2D[] hits;
    public GameObject effect;
    GameObject arrow;
    public float speed;
    public AudioClip skillSFX;

    // Start is called before the first frame update
    void Start()
    {
        hitList = new List<GameObject>();
        Destroy(gameObject, 1.0f);
        arrow = Instantiate(effect, transform.position + new Vector3(0.0f, -0.2f, 0.0f), transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        Destroy(arrow, 1.0f);
        if (direction == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            effect.GetComponent<SpriteRenderer>().flipX = true;
            speed = -20.0f;
        }
        if (direction == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            effect.GetComponent<SpriteRenderer>().flipX = false;
            speed = 20.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if(speed < 0)
        {
            hits = Physics2D.RaycastAll(transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Enemy"));
            arrow.transform.position = transform.position + new Vector3(0.0f, 0.2f, 0.0f);
            Debug.Log("Go");
        }
        else
        {
            hits = Physics2D.RaycastAll(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Enemy"));
            arrow.transform.position = transform.position + new Vector3(0.0f, -0.2f, 0.0f);
            Debug.Log("Go");
        }
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hitList.Contains(hit.collider.gameObject) == false)
            {
                hit.collider.gameObject.GetComponent<Enemy>().damaged(damage * 2);
                hitList.Add(hit.collider.gameObject);
            }
        }

        foreach(GameObject obj in hitList)
        {
            if(obj != null)
                obj.transform.position = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z);
                obj.GetComponent<Enemy>().damaged(0);
        }
    }
}
