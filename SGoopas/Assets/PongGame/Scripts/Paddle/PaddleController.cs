using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    public GameObject paddle;
    public float zMin;
    public float zMax;
    public float speedLimit;

    // Move paddle in the z direction by the given amount
    public void MovePaddle(float amount)
    {
        if (amount != 0f)
        {
            Vector3 position = paddle.transform.position;
            if (amount > speedLimit) amount = speedLimit;
            if (amount < -speedLimit) amount = -speedLimit;
            position.z = Mathf.Clamp(position.z + amount, zMin, zMax);
            paddle.transform.position = position;
        }
    }
}
