using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartMovement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartMovement()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(-100, 0, 0));
    }
    void StartRandomMovement()
    {
        float angle = Random.value * 360;
        //TODO: apply this force in the direction of the randomly generated angle
        this.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0));
    }
}
