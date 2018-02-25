using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour {

    public bool hit;
    public Collision col;
	void Start () {
        hit = false;
	}

    private void OnCollisionStay(Collision collision)
    {
        hit = true;
        col = collision;
    }
    private void OnCollisionExit(Collision collision)
    {
        hit = false;
    }
}
