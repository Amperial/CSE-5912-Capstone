using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour {

    private Rigidbody rb;
    private bool jump;
    private float velocity;
    private Transform child;
    private Quaternion forward, back, left, right;
    private float deltaT;
    private Vector3 forwardForce, backForce, rightForce, leftForce;

	void Start () {
        rb = transform.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jump = false;
        velocity = 5f;
        child = transform.GetChild(0);
        forward = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        back = Quaternion.LookRotation(Vector3.back, Vector3.up);
        left = Quaternion.LookRotation(Vector3.left, Vector3.up);
        right = Quaternion.LookRotation(Vector3.right, Vector3.up);
        forwardForce = new Vector3(0f, 0f, 60f);
        backForce = new Vector3(0f, 0f, -45f);
        rightForce = new Vector3(50f, 0f, 0f);
        leftForce = new Vector3(-50f, 0f, 0f);
    }

	// FixedUpdate for physics update
	void FixedUpdate () {
        if(Input.GetKey(KeyCode.W) && rb.velocity.magnitude < velocity)
        {
            rb.AddForce(forwardForce);
            if (child.rotation != forward) 
            {
                deltaT = 0.5f;
                if (Mathf.Abs(Quaternion.Angle(child.rotation, forward)) < 90f)
                    deltaT = 0.35f;
                child.localRotation = Quaternion.Lerp(child.rotation, forward, deltaT);
            }
            
        }
        else if(Input.GetKey(KeyCode.S) && rb.velocity.magnitude < velocity)
        {
            rb.AddForce(backForce);
            if (child.rotation != back) 
            {
                deltaT = 0.5f;
                if (Mathf.Abs(Quaternion.Angle(child.rotation, back)) < 90f)
                    deltaT = 0.35f;
                child.localRotation = Quaternion.Lerp(child.rotation, back, deltaT);
            }         
        }

        if (Input.GetKey(KeyCode.D) && rb.velocity.magnitude < velocity)
        {
            rb.AddForce(rightForce);
            if (child.rotation != right) 
            {
                deltaT = 0.5f;
                if (Mathf.Abs(Quaternion.Angle(child.rotation, right)) < 90f)
                    deltaT = 0.35f;
                child.localRotation = Quaternion.Lerp(child.rotation, right, deltaT);
            }
        }
           
        else if (Input.GetKey(KeyCode.A) && rb.velocity.magnitude < velocity)
        {
            rb.AddForce(leftForce);
            if (child.rotation != left) 
            {
                deltaT = 0.5f;
                if (Mathf.Abs(Quaternion.Angle(child.rotation, left)) < 90f)
                    deltaT = 0.35f;
                child.localRotation = Quaternion.Lerp(child.rotation, left, deltaT);
            }
        }
            

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
