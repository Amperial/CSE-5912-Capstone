using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

    private bool grab,trigger;
    private Collider item;
    GameObject parent;
    Transform previousParentTransform;
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
        // No-op.
    }
    public void PutDown()
    {
        // No-op.
    }
    public void Grab()
    {
        if(!grab && trigger)
        {
            parent.AddComponent<FixedJoint>();
            Joint joint = parent.GetComponent<Joint>();
            joint.connectedBody = item.attachedRigidbody;
            grab = true;
        }
    }
    public void Release()
    {
        if(grab)
        {
            Destroy(parent.GetComponent<Joint>());
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
