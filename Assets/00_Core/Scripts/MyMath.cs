// Author : Auguste Paccapelo

using UnityEngine;

public struct MyMath
{
    static public Vector2 PolarToCart(float angle, float distance)
    {
        angle *= Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
    }

    static public float GetVectorAngleDegree(Vector2 vect)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vect.y, vect.x);
    }
}