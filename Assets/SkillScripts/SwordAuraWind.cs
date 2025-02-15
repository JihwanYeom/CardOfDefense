using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAuraWind : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.right * 15.0f * Time.deltaTime;
    }
}
