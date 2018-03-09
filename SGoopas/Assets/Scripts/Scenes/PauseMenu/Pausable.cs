using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pausable : MonoBehaviour {
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MasterStateMachine.Instance.pause();
        }
	}
}
