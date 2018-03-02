using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Grabbing : MonoBehaviour {
    public static Grabbing Instance;

    private bool triggerActivated;
    private Collider item;

    public delegate void GrabAvailabilityChanged(bool availability, Collider grabbableObject);
    public static event GrabAvailabilityChanged grabEvent;

	void OnTriggerStay(Collider other){
		if (other.attachedRigidbody != null && !triggerActivated) {
            grabEvent(true, other);
            triggerActivated = true;
		}
	}

    void OnTriggerExit(Collider other)
    {
        grabEvent(false, other);
        triggerActivated = false;
    }
}
