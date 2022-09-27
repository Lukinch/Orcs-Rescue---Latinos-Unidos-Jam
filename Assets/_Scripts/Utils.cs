using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExtensions
{
    public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange = 0.0001f)
    {
        return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
    }
}
