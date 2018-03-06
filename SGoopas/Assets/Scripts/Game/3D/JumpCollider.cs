using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour {

    public Collision col;
	void Start () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        col = collision;
    }

}
