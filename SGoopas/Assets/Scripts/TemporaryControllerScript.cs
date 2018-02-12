using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryControllerScript : MonoBehaviour {
    private bool is2D;
	private bool firstFrame = true;
    public GameObject triggerable;
	// Use this for initialization
	void Start () {
        is2D = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (firstFrame) {
			// Default to 3D mode initially, but wait until everything else is loaded first.
			// There is probably a better way to do this..
			this.BroadcastMessage ("SwitchTo3D");
			firstFrame = false;
		}

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            triggerable.GetComponent<ITriggerable>().Trigger();
        }
    }
}
