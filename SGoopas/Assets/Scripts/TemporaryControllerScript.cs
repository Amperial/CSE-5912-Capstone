using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryControllerScript : MonoBehaviour {
    private bool is2D;
	// Use this for initialization
	void Start () {
        is2D = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("SwapDimension"))
        {
            if (is2D)
            {
                is2D = false;
                this.BroadcastMessage("SwitchTo3D");
            } else
            {
                is2D = true;
                this.BroadcastMessage("SwitchTo2D");
            }
        }
    }
}
