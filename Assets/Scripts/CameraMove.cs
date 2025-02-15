using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        x=x*moveSpeed*Time.deltaTime;
        if(x!=0) CardManager.Inst.CardAlignment();
        transform.Translate(Vector2.right * x);
        // if(x!=0) CardManager.Inst.CardAlignment();
    }
}
