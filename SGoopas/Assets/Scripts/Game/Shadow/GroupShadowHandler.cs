using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupShadowHandler : MonoBehaviour {
	public GameObject shadowObjectsParent;
	public bool isLightMovable = false;
	// Be explicit with reference to the light in the editor.
	public Light shadowLight;
	public GameObject shadowPlane;

	private List<ShadowController> shadowControllers = new List<ShadowController>();

	public void Start() {
		foreach (ShadowConfiguration configuration in shadowObjectsParent.GetComponentsInChildren<ShadowConfiguration>()) {
			if(isLightMovable){
				configuration.objectType = ShadowConfiguration.ShadowObjectType.Dynamic;
			}
			ShadowController controller = ShadowControllerFactory.CreateControllerFromConfiguration (configuration, shadowLight, shadowPlane);
			shadowControllers.Add (controller);
		}
	}

	public void SwitchTo2D() {
		foreach (ShadowController shadowController in shadowControllers) {
			shadowController.ConstructShadow();
		}
	}

	public void SwitchTo3D() { 
		foreach (ShadowController shadowController in shadowControllers) {
			shadowController.DeconstructShadow();
		}
	}
}
