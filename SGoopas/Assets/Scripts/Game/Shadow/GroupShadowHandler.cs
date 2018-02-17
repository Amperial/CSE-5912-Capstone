using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupShadowHandler : MonoBehaviour {
	public GameObject shadowObjectsParent;

	// Be explicit with reference to the light in the editor.
	public Light shadowLight;
	public GameObject shadowPlane;

	public void Start() {
		foreach (ShadowController shadowController in shadowObjectsParent.GetComponentsInChildren<ShadowController>()) {
			shadowController.ConfigureWithLightParams(shadowLight, shadowPlane);
		}
	}

	public void SwitchTo2D() {
		foreach (ShadowController shadowController in shadowObjectsParent.GetComponentsInChildren<ShadowController>()) {
			shadowController.ConstructShadow();
		}
	}

	public void SwitchTo3D() { 
		foreach (ShadowController shadowController in shadowObjectsParent.GetComponentsInChildren<ShadowController>()) {
			shadowController.DeconstructShadow();
		}
	}
}
