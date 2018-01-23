using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSaveAndLoad : MonoBehaviour {
    private Vector3 velocity;
    private Vector3 position;
    private Vector3 angularVelocity;
    private new Rigidbody rigidbody;
    private BallMove movement;

    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        movement = gameObject.GetComponent<BallMove>();
    }
	void Save()
    {
        velocity = rigidbody.velocity;
        angularVelocity = rigidbody.angularVelocity;
        position = transform.position;
    }

    void Load()
    {
        movement.CancelInvoke();
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
        transform.position = position;
        if(velocity.magnitude <= 0.0001 && velocity.magnitude >= -0.0001)
        {
            movement.Invoke("StartRandomMovement", 1f);
        }
    }
}
