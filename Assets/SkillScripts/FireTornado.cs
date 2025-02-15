using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : MonoBehaviour
{
    public GameObject effect;
    public Vector2 range;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tornado = Instantiate(effect, transform.position + new Vector3(0, 0, 0), transform.rotation);
        Destroy(tornado, 5.0f);
    }
}
