using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaddleController : PaddleController {

    public float sensitivity = 2f;
	
	void Update () {
        MovePaddle(Input.GetAxis("Mouse Y") * sensitivity);
	}
}
