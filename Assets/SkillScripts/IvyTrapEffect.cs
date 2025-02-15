using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyTrapEffect : MonoBehaviour
{
    List<GameObject> hitList;
    public RaycastHit2D hit;
    public GameObject effect;
    public AudioClip skillSFX;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        hitList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 10.0f * Time.deltaTime;
        hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null && hitList.Contains(hit.collider.gameObject) == false)
        {
            AudioManager.Instance.PlaySFX(skillSFX);
            hit.collider.gameObject.GetComponent<Enemy>().damaged((int)(damage*0.5f));
            hit.collider.gameObject.GetComponent<Enemy>().move_speed = 1;
            GameObject ivy = Instantiate(effect, hit.collider.transform.position + new Vector3(0, 2.0f, 0), transform.rotation);
            ivy.transform.SetParent(hit.collider.transform);
            Destroy(ivy, 5.0f);
            hitList.Add(hit.collider.gameObject);
        }
    }
}
