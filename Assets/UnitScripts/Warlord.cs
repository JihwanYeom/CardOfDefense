using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlord : Knight
{
    // Start is called before the first frame update
    void Start()
    {   
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

  
    public override void cast()
    {
        Skill info = skill.GetComponent<Skill>();
        info.damage = atk;
        anim.SetTrigger("6_Skill");
        GameObject s = Instantiate(skill, transform.position, transform.rotation);
        s.transform.SetParent(transform);
        s.transform.position = transform.position + info.pos;
    }
}
