using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DConfig : MonoBehaviour {
    public float airMoveForce = 1140f;
    public float jumpForce = 700f;
    public float airDashForce = 1000f;
    public float walkForce = 3000f;
    public float dJumpForce = 500f;
    public float maxHoriSpeed = 10f;
    public float maxVertSpeed = 100f;
    public float dashStartAngle = 90f;
    public float dashDistance = 5f;
    public float enemyDetectionAngle = 15f;
    public float dashTime = 0f;
}
