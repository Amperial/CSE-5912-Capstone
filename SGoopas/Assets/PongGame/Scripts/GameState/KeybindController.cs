using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindController : MonoBehaviour {

    public GameObject ball;
    public KeyCode resetBall = KeyCode.O;
    public KeyCode speedUpBall = KeyCode.I;
    public float speedMultiplier = 1.5f;

    public GameObject computerPaddle;
    public KeyCode controlComputer = KeyCode.P;
    
    void Update () {
        if (Input.GetKeyDown(resetBall))
        {
            ball.GetComponent<BallRestart>().RestartGame();
        } else if (Input.GetKeyDown(speedUpBall))
        {
            Rigidbody ballBody = ball.GetComponent<Rigidbody>();
            ballBody.velocity = ballBody.velocity * speedMultiplier;
        }
        if (Input.GetKeyDown(controlComputer))
        {
            AIPaddleController aiController = computerPaddle.GetComponent<AIPaddleController>();
            KeyboardPaddleController keyboardController = computerPaddle.GetComponent<KeyboardPaddleController>();
            if (aiController == null)
            {
                aiController = gameObject.AddComponent<AIPaddleController>();
                aiController.paddle = computerPaddle;
                Destroy(keyboardController);
            } else
            {
                keyboardController = gameObject.AddComponent<KeyboardPaddleController>();
                keyboardController.paddle = computerPaddle;
                Destroy(aiController);
            }
        }
    }
}
