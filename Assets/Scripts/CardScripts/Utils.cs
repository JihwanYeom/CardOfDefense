using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[System.Serializable]
public class PRS
{
    public Vector3 pos;
    public quaternion rot;
    public Vector3 scale;

    public PRS(Vector3 pos, quaternion rot, Vector3 scale)
    {
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
    }
}

public class Utils
{
    public static Quaternion QI => quaternion.identity;

    public static Vector3 MousePos
    {
        get
        {
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z=-10;
            return result;
        }
    }
}


