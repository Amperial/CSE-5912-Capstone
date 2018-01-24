using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPaddleController : PaddleController {
    
    public KeyCode upKey;
    public KeyCode downKey;

    void Update() {
        float move = 0f;
        if (Input.GetKey(upKey))
        {
            move += speedLimit;
        }
        if (Input.GetKey(downKey))
        {
            move -= speedLimit;
        }
        MovePaddle(move);
        
    }
}
