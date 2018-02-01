using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelpers {
    /*
        A helper method to translate a rotation represented by a 4x4 matrix into a quaternion for setting an objects transform
    */
    public static Quaternion QuaternionFromMatrix(Matrix4x4 m)
    {
        //Given in top answer to: https://answers.unity.com/questions/11363/converting-matrix4x4-to-quaternion-vector3.html
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
        q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
        q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
        return q;
    }

    /*
        Untiy doesn't have a cross product implementation for 2d vectors (as it's not technically defined),
        so this is a helper method to find the scalar z component of the would-be cross product in 3d space.
    */
    public static double CrossProduct(Vector2 p1, Vector2 p2)
    {
        return (p1.x * p2.y - p1.y * p2.x);
    }
}
