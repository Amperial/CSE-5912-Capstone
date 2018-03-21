using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
	public enum MovementDirection { Vertical, Horizontal };
	public MovementDirection movementDirection;
    public float speed; //replace with privates, instantiated in Start()?
    public float travelDistance; //replace with privates, instantiated in Start()?
    private Rigidbody platform;
	private float angle = 0;
	private Vector3 origin;
	void Start () {
        platform = gameObject.AddComponent<Rigidbody>();
		platform.useGravity = false;
		platform.isKinematic = true;
		origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}

	void Update(){
		angle += speed * Time.deltaTime;
		if (angle > 2 * Mathf.PI) {
			angle = 0;
		}
	}

    
    private void FixedUpdate()
    {
		if (movementDirection == MovementDirection.Vertical) {
			platform.position = new Vector3(origin.x, 
											origin.y + (Mathf.Sin (angle) * travelDistance), 
											origin.z);
		} else {
			platform.position = new Vector3(origin.x + (Mathf.Sin (angle) * travelDistance), 
				origin.y, 
				origin.z);
		}
	}
}
