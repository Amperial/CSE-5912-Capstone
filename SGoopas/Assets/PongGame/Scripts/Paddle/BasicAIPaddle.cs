using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIPaddle : PaddleController {

    public GameObject ball;
	
	void Update () {
        float paddleZ = paddle.transform.position.z;
        float ballZ = ball.transform.position.z;
        MovePaddle(ballZ - paddleZ);
	}
}
