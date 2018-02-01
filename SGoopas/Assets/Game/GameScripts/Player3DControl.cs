using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 5f;
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal * speed * 20f, 0f, moveVertical * speed * 20f));

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(0f, speed * 100f, 0f));
    }
}
