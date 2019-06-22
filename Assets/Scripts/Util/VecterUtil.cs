using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VecterUtil
{
    public static Vector3 V2ToV3(Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
