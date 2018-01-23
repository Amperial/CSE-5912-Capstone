using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {
    public float initialSpeed;
	// Use this for initialization
	void Start () {
        StartMovement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartMovement()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(-initialSpeed, 0, 0));
    }
    void StartRandomMovement()
    {
        float angle = Random.value * 60;
        angle = angle - 30;
        angle = (float)(Mathf.PI / 180.0 * angle);
        this.GetComponent<Rigidbody>().AddForce(Vector3.RotateTowards(new Vector3(-initialSpeed, 0, 0), new Vector3(0,0,initialSpeed), angle, initialSpeed));
    }
}
