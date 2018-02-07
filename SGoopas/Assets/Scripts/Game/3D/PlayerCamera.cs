using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject played3D;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float posDiv = played3D.transform.position.x - transform.position.x;
        transform.Translate(new Vector3(posDiv, 0f, 0f));
	}
}
