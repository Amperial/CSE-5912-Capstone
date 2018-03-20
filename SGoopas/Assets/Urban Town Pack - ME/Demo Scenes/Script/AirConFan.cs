using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirConFan : MonoBehaviour {

    public float speed;

	void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
	}
}
