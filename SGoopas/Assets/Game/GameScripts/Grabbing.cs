using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

    private bool grab,trigger;
    private Collider item;
	void Start () {
        grab = false;
        trigger = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.R) && grab)
        {
            GameObject parent = transform.parent.gameObject;
            Destroy(parent.GetComponent<HingeJoint>());
            item.attachedRigidbody.useGravity = true;
            grab = false;
        }
        if (Input.GetKeyDown(KeyCode.G) && !grab && trigger)
        {
            Debug.Log("GRAB INITIATED");
            GameObject parent = transform.parent.gameObject;

            parent.AddComponent<HingeJoint>();

            HingeJoint joint = parent.GetComponent<HingeJoint>();
            joint.connectedBody = item.attachedRigidbody;
            item.attachedRigidbody.useGravity = false;

            JointSpring hingeSpring = joint.spring;
            hingeSpring.spring = 10;
            hingeSpring.damper = 3;
            hingeSpring.targetPosition = 0;
            joint.spring = hingeSpring;
            joint.useSpring = true;

            grab = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && grab)
        {
            HingeJoint joint = transform.parent.gameObject.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if(hingeSpring.targetPosition < 60)
                hingeSpring.targetPosition += 10;
            joint.spring = hingeSpring;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grab)
        {
            HingeJoint joint = transform.parent.gameObject.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if(hingeSpring.targetPosition > 10)
                hingeSpring.targetPosition -= 10;
            joint.spring = hingeSpring;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        trigger = true;
        item = other;
    }

    void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

}
