using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

    private bool grab,trigger;
    private Collider item;
	GameObject parent;
    public bool Grabbable
    {
        get
        {
            return trigger;
        }
    }
	void Start () {
        grab = false;
        trigger = false;
		parent = transform.parent.gameObject;
	}
	
    public void lift()
    {
        if(grab)
        {
            HingeJoint joint = parent.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if (hingeSpring.targetPosition < 60)
                //increment the angle so that object is lifed. maximum is 60 degree
                hingeSpring.targetPosition += 10;
            joint.spring = hingeSpring;
        }
    }
    public void PutDown()
    {
        if (grab)
        {
            HingeJoint joint = parent.GetComponent<HingeJoint>();

            JointSpring hingeSpring = joint.spring;
            if (hingeSpring.targetPosition > 10)
                hingeSpring.targetPosition -= 10;
            joint.spring = hingeSpring;
        }
    }
    public void Grab()
    {
        if(!grab && trigger)
        {
            parent.AddComponent<HingeJoint>();

            HingeJoint joint = parent.GetComponent<HingeJoint>();
            joint.connectedBody = item.attachedRigidbody;

            //added spring element to the joint so that the weight of the lifted object can have gameplay effect
            JointSpring hingeSpring = joint.spring;
            //spring coefficient
            hingeSpring.spring = 20;
            //damping coefficient
            hingeSpring.damper = 3;
            //initial joint angle when grabbing; object right in front of player
            hingeSpring.targetPosition = 0;
            joint.spring = hingeSpring;
            joint.useSpring = true;

            grab = true;
        }
    }
    public void Release()
    {
        if(grab)
        {
            Destroy(parent.GetComponent<HingeJoint>());
            grab = false;
        }
    }
	void OnTriggerStay(Collider other){

		if (!grab && other.attachedRigidbody != null) {
			trigger = true;
			item = other;
		}
	}

    void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

}
