using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject player3D;
    public GameObject player2D;

    private bool is2D;
	Vector3 initialCameraOffset3D;

	private GameObject relevantGameObject;

	void Start () {
		relevantGameObject = player3D;
		initialCameraOffset3D = player3D.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posDiv = (relevantGameObject.transform.position - transform.position) - initialCameraOffset3D;
		transform.Translate(posDiv.x, 0f, posDiv.z, Space.World);
	}

    public void SwitchTo2D()
    {
		relevantGameObject = player2D;
    }

    public void SwitchTo3D()
    {
		relevantGameObject = player3D;
    }
}
