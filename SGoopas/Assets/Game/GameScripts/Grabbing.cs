using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

    private bool grab,trigger;
    private Collider item;
	GameObject parent;

	void Start () {
        grab = false;
        trigger = false;
		parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R) && grab)
        {
			item.attachedRigidbody.useGravity = true;
            Destroy(parent.GetComponent<HingeJoint>());
            grab = false;
        }
        if (Input.GetKeyDown(KeyCode.G) && !grab && trigger)
        {
            Debug.Log("GRAB INITIATED");

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
            HingeJoint joint = parent.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if(hingeSpring.targetPosition < 60)
                hingeSpring.targetPosition += 10;
            joint.spring = hingeSpring;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grab)
        {
            HingeJoint joint = parent.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if(hingeSpring.targetPosition > 10)
                hingeSpring.targetPosition -= 10;
            joint.spring = hingeSpring;
        }
    }

	void OnTriggerStay(Collider other){

		if (!grab) {
			trigger = true;
			item = other;
		}
	}

    void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

}
