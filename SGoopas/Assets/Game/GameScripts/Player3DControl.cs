using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 5f;

	private bool grab = false;

	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		/* Press R if player is attached to an object (grabbing) to dettach (release)*/
		if (Input.GetKey (KeyCode.R) && grab) {
			Destroy (gameObject.GetComponent<FixedJoint> ());
			grab = false;
		}
	}

	// FixedUpdate for physics update
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal * speed * 20f, 0f, moveVertical * speed * 20f));

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(0f, speed * 100f, 0f));
    }

	// Needs to be better implemented since right now one has to be pressing G while moving into an object to register grabbing
	// Instead of being able to grab while near it as well
	void OnCollisionEnter (Collision collision) {
		if (Input.GetKey (KeyCode.G)) {
			gameObject.AddComponent<FixedJoint> ();
			gameObject.GetComponent<FixedJoint> ().connectedBody = collision.rigidbody;
			grab = true;
		}
	}
}
