using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveAndLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BroadcastMessage("Save");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BroadcastMessage("Save");
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            BroadcastMessage("Load");
        }
	}
}
