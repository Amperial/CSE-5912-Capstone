using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelpers {
    /*
        A helper method to translate a rotation represented by a 4x4 matrix into a quaternion for setting an objects transform
    */
    public static Quaternion QuaternionFromMatrix(Matrix4x4 matrix)
    {
        // Taken from https://forum.unity.com/threads/how-to-assign-matrix4x4-to-transform.121966/

        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
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
