/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public int max_hp;
    public int atk;
    public int def;
    public int hp;
    public float speed;
    public bool inBattle;
    RaycastHit2D target;

    // Start is called before the first frame update
    void Start()
    {
        name = "Unit";
        max_hp = 100;
        atk = 40;
        def = 5;
        speed = 2.0f;
        
        hp = 100;
        inBattle = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.right * 1, new Color(1, 0, 0));
        target = Physics2D.Raycast(transform.position, Vector2.right * 1, 1, LayerMask.GetMask("Enemy"));


        if (target.collider != null)
        {
            if (!inBattle)
                StartCoroutine(attack());
            inBattle = true;
        }
        else
        {
            if (inBattle)
                StopCoroutine(attack());
            inBattle = false;
            move();
        }

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    public void move()
    {
        Vector2 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;
    }

    
    public virtual IEnumerator attack()
    {
        GameObject targetObject = target.collider.gameObject;
        EnemyManager status = targetObject.GetComponent<EnemyManager>();
        HpBar hpBar = targetObject.GetComponentInChildren<HpBar>();
        

        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            status.hp = status.hp - atk;
            hpBar.SetHealth(hp, hp);

            if (status.hp < 0)
            {
                StopCoroutine(attack());
                Destroy(targetObject);
                break;
            }
        }
        
    }
}
*/