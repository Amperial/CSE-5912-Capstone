using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    public GameObject paddle;
    public float zMin = -3.75f;
    public float zMax = 3.75f;
    public float speedLimit = 0.1f;

    // Move paddle in the z direction by the given amount
    public void MovePaddle(float amount)
    {
        if (amount != 0f)
        {
            Vector3 position = paddle.transform.position;
            amount = Mathf.Clamp(amount, -speedLimit, speedLimit);
            position.z = Mathf.Clamp(position.z + amount, zMin, zMax);
            paddle.transform.position = position;
        }
    }
}
