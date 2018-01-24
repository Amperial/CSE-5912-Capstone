using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSaveAndLoad : MonoBehaviour {

    private Vector3 velocity;
    private Vector3 position;
    private Vector3 angularVelocity;
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    void Save()
    {
        velocity = rigidbody.velocity;
        angularVelocity = rigidbody.angularVelocity;
        position = transform.position;
    }

    void Load()
    {
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
        transform.position = position;
    }
}
