using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour, IStateEventDelegate {
    public static Grabbing Instance;

    private bool trigger;
    private Collider item;

    private void Awake()
    {
        Instance = this;
    }

    public bool CurrentState() {
        return trigger;
    }

    public Object StateObject() {
        return item;
    }

	void OnTriggerStay(Collider other){
		if (other.attachedRigidbody != null) {
			trigger = true;
			item = other;
		}
	}

    void OnTriggerExit(Collider other)
    {
        trigger = false;
    }
}
