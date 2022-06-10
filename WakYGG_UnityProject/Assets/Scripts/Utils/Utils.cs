using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 NewVector2(float x, float y)
    {
        return Vector2.right * x + Vector2.up * y;
    }
}