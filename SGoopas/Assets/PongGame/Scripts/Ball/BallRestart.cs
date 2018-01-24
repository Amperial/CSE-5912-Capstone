﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRestart : MonoBehaviour {

    private Vector3 ballStart;
    void Start()
    {
        ballStart = transform.position;
    }
    public void RestartGame()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0f, 0f, 0f);
        rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.position = ballStart;
        BallMove movement = gameObject.GetComponent<BallMove>();
        movement.Invoke("StartRandomMovement", 1f);
    }
}