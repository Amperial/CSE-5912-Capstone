using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleRestart : MonoBehaviour {

    private Vector3 paddleStart;
    void Start()
    {
        paddleStart = transform.position;
    }
    public void RestartGame()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0f, 0f, 0f);
        rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.position = paddleStart;
    }
}
