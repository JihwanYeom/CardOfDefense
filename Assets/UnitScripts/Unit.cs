
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States {WAIT, APPROACH, BATTLE, RETURN}
public enum Direction { LEFT, RIGHT }

public class Unit : MonoBehaviour
{
    public string name;
    public int hp;
    public int maxhp;
    public int atk;
    public int def;
    public float attack_speed;
    public float move_speed;
    public float range;
    public float vision;
    public States state;
    public Enemy target;


    public Vector3 scale;
    public Vector2 spawnPoint;
    public Direction direction;
    public Animator anim;
    public GameObject effect;
    public GameObject skill;
    public AudioClip attackSFX;


    public int job_class;
    public int job_route;
    public int level;

    public void Start()
    {
        spawnPoint = transform.position;
        scale = transform.localScale;
        direction = Direction.RIGHT;
        state = States.WAIT;
        anim = GetComponentInChildren<Animator>();
        //StartCoroutine(useSkill());
    }

    // Update is called once per frame
    public void Update()
    {
        detect();
        move();
    }

    public void move()
    {
        if(state == States.APPROACH)
        {
            if (transform.position.x - target.transform.position.x >= 0)
                setDirection(Direction.LEFT);
            else
                setDirection(Direction.RIGHT);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, move_speed * Time.deltaTime);
        }
        if(state == States.RETURN)
        {
            if (transform.position.x - spawnPoint.x >= 0)
                setDirection(Direction.LEFT);
            else
                setDirection(Direction.RIGHT);
            transform.position = Vector2.MoveTowards(transform.position, spawnPoint, move_speed * Time.deltaTime);
        }
        if (state == States.WAIT) 
        {
            setDirection(Direction.RIGHT);
        }
        if (state == States.BATTLE)
        {
            if (transform.position.x - target.transform.position.x >= 0)
                setDirection(Direction.LEFT);
            else
                setDirection(Direction.RIGHT);
        }
    }

    public Coroutine attackCoroutine;
    public void detect()
    {
        Vector3 rayPos = transform.position;
        rayPos.y = 1;

        Debug.DrawRay(rayPos, Vector2.right * vision, new Color(0, 0, 1));
        Debug.DrawRay(rayPos, Vector2.left * vision, new Color(0, 0, 1));
        Debug.DrawRay(rayPos, Vector2.right * range, new Color(1, 0, 0));
        Debug.DrawRay(rayPos, Vector2.left * range, new Color(1, 0, 0));
        

        RaycastHit2D leftHit = Physics2D.Raycast(rayPos, Vector2.left, vision, LayerMask.GetMask("Enemy"));
        RaycastHit2D rightHit = Physics2D.Raycast(rayPos, Vector2.right, vision, LayerMask.GetMask("Enemy"));
        GameObject leftTarget, rightTarget;


        if (target != null && target.gameObject == null)
        {
            target = null;
        }


        if (leftHit.collider != null)
            leftTarget = leftHit.collider.gameObject;
        else
            leftTarget = null;

        if (rightHit.collider != null)
            rightTarget = rightHit.collider.gameObject;
        else
            rightTarget = null;
        
        if (leftTarget != null && rightTarget != null)
        {
            float leftDistance = Vector2.Distance(transform.position, leftTarget.transform.position);
            float rightDistance = Vector2.Distance(transform.position, rightTarget.transform.position);

            if (leftDistance < rightDistance)
                target = leftTarget.GetComponent<Enemy>();
            else
                target = rightTarget.GetComponent<Enemy>();
        }
        else if (leftTarget != null)
        {
            target = leftTarget.GetComponent<Enemy>();
        }
        else if (rightTarget != null) 
        {
            target = rightTarget.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < range + target.transform.localScale.y/2)
            {
                if (state != States.BATTLE) 
                {
                    anim.SetBool("1_Move", false);
                    attackCoroutine = StartCoroutine(attack());
                }
                state = States.BATTLE;
            }
            else
            {
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    attackCoroutine = null;
                }
                if (state != States.APPROACH)
                {
                    anim.SetBool("1_Move", true);
                    state = States.APPROACH;
                }
            }
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            float dist = (float)System.Math.Sqrt((transform.position.x - spawnPoint.x) * (transform.position.x - spawnPoint.x));
            if (dist < 0.1f)
            {
                if(state != States.WAIT)
                {
                    state = States.WAIT;
                    anim.SetBool("1_Move", false);
                }
            }
            else
            {
                if (state != States.RETURN)
                {
                    state = States.RETURN;
                    anim.SetBool("1_Move", true);
                }
            }
        }
    }

    public virtual IEnumerator attack()
    {
        while (true)
        {
            if (target != null)
            {
                anim.SetTrigger("2_Attack");
                yield return new WaitForSeconds(0.5f);
                AudioManager.Instance.PlaySFX(attackSFX);
                if (direction == Direction.LEFT)
                {
                    effect.GetComponent<SpriteRenderer>().flipX = true;
                    GameObject slash = Instantiate(effect, transform.position + new Vector3(-2.0f, 1.0f, 0), transform.rotation);
                    Destroy(slash, 0.5f);
                }
                if (direction == Direction.RIGHT)
                {
                    effect.GetComponent<SpriteRenderer>().flipX = false;
                    GameObject slash = Instantiate(effect, transform.position + new Vector3(2.0f, 1.0f, 0), transform.rotation);
                    Destroy(slash, 0.5f);
                }
                
                int damage = atk - target.def;
                if (damage < 0)
                    damage = 0;
                target.damaged(damage);
                yield return new WaitForSeconds(1.0f / attack_speed);
            }
        }
    }

    public virtual void cast()
    {
        Skill info = skill.GetComponent<Skill>();
        info.damage = atk;

        anim.SetTrigger("6_Skill");
        GameObject s = Instantiate(skill, transform.position + info.pos, transform.rotation);

    }

    public IEnumerator useSkill()
    {
        while(true)
        {
            cast();
            yield return new WaitForSeconds(3.0f);
        }
    }

    public virtual void damaged(int damage)
    {
        HpBar hpBar = GetComponentInChildren<HpBar>();
        hp -= damage;
        //anim.SetTrigger("3_Damaged");
        if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("4_Death");
            move_speed = 0;
            Destroy(gameObject, 0.8f);
        }
        hpBar.SetHealth(hp, maxhp);
    }

    public void setDirection(Direction dir)
    {
        Vector3 cur = transform.localScale;
        Transform hpBar = transform.Find("HpBar");
        SpriteRenderer hpSprite = hpBar.GetComponent<SpriteRenderer>();
        if (dir == Direction.LEFT)
        {
            transform.localScale = new Vector3(scale.x, cur.y, cur.z);
            hpBar.localScale = new Vector3(0.8f, 0.8f, 0);

            direction = Direction.LEFT;
        }
        else
        {
            transform.localScale = new Vector3(-scale.x, cur.y, cur.z);
            hpBar.localScale = new Vector3(-0.8f, 0.8f, 0);
            direction = Direction.RIGHT;
        }
    }

}
