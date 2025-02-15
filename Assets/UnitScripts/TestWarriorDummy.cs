using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWarriorDummy : Enemy
{
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        detect();
    }
}
