﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 5f;

	private bool grab = false;

	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}

	// FixedUpdate for physics update
	void FixedUpdate () {
        if(Input.GetKey(KeyCode.W))
            rb.AddRelativeForce(new Vector3(0f, 0f, speed * 5f));
        else if(Input.GetKey(KeyCode.S))
            rb.AddRelativeForce(new Vector3(0f, 0f, speed * -5f));

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, 1f, 0));
        else if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -1f, 0));

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(0f, speed * 100f, 0f));
    }

}
