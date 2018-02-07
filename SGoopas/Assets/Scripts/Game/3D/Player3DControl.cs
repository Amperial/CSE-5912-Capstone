using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour {

    private Rigidbody rb;
    private bool jump;

	void Start () {
        rb = transform.GetComponent<Rigidbody>();
        jump = false;
	}

	// FixedUpdate for physics update
	void FixedUpdate () {
        if(Input.GetKey(KeyCode.W))
            rb.AddRelativeForce(new Vector3(0f, 0f, 60f));
        else if(Input.GetKey(KeyCode.S))
            rb.AddRelativeForce(new Vector3(0f, 0f, -45f));

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, 1.5f, 0));
        else if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -1.5f, 0));

        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            rb.AddForce(new Vector3(0f, 300f, 0f));
            jump = false;
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        jump = true;
    }

}
