using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddleController : PaddleController {

    public GameObject ball;

    void Start()
    {
        if (ball == null)
        {
            ball = GameObject.Find("Ball");
        }
    }

    void Update () {
        float paddleZ = paddle.transform.position.z;
        float ballZ = ball.transform.position.z;
        MovePaddle(ballZ - paddleZ);
    }
}
