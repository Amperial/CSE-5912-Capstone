using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    public float speed; //replace with privates, instantiated in Start()?
    public float travelDistance; //replace with privates, instantiated in Start()?
    //Starting position
    private float minHeight;
    //Max height position
    private float maxHeight;
    private Vector3 velocity;
    private Rigidbody platform;
	
	void Start () {
        //get the starting position as a point of reference
        minHeight = gameObject.transform.position.y;
        maxHeight = travelDistance + minHeight;
        velocity = new Vector3(0, speed, 0);

        gameObject.AddComponent<Rigidbody>();
        platform = gameObject.GetComponent<Rigidbody>();
	}

    
    private void FixedUpdate()
    {
        if (gameObject.transform.position.y < minHeight)
        {
            velocity = new Vector3(0, speed, 0);
        }else if(gameObject.transform.position.y > maxHeight)
        {
            velocity = new Vector3(0, -speed, 0);
        }

        platform.velocity = velocity;
    }
}
