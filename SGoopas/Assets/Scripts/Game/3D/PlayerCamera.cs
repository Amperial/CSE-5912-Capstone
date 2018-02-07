using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject played3D;
    public GameObject played2D;

    private bool is2D;
    private float posDiv;

	void Start () {
        is2D = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (is2D)
        {
            posDiv = played2D.transform.position.x - transform.position.x;
        }
        else
        {
            posDiv = played3D.transform.position.x - transform.position.x;
        }
        transform.Translate(new Vector3(posDiv, 0f, 0f));
	}

    public void SwitchTo2D()
    {
        is2D = true;
        Debug.Log("2D");
    }

    public void SwitchTo3D()
    {
        is2D = false;
        Debug.Log("3D");
    }
}
