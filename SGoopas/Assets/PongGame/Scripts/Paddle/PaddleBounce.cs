using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounce : MonoBehaviour {
    public float speedUpAmount;
    public float changeDirectionAmount;

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name.Equals("Ball"))
        {
            GameObject ball = other.gameObject;
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            Vector3 changeDirection = new Vector3(0, 0, changeDirectionAmount * (ball.transform.position.z - transform.position.z));
            Vector3 speedUp;
            if (ball.transform.position.x > transform.position.x)
            {
                speedUp = new Vector3(speedUpAmount, 0, 0);
            } else
            {
                speedUp = new Vector3(-speedUpAmount, 0, 0);
            }
            ballRigidbody.AddForce(changeDirection + speedUp);
        }
    }
    
}
