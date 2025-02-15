using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyTrap : Skill
{
    public GameObject effect;
    public AudioClip skillSFX;

    void Start()
    {
        GameObject trap = Instantiate(effect, transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX(skillSFX);
        Destroy(trap, 5.0f);
    }
}
