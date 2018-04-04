using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
need to know state of child triggers

*/
public class SwitchContainer : MonoBehaviour {
	//public int solution = 0;
	protected List<bool> triggerStates = new List<bool>();

	// Use this for initialization
	protected void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			GameObject child = transform.GetChild (i).gameObject;
			SwitchTrigger switchTrigger = child.GetComponentInChildren<SwitchTrigger> ();
			switchTrigger.SetIndexInParent (i);
			triggerStates.Add (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual bool NotifyTriggerStateChange(int index){
		return false;
	}
}
