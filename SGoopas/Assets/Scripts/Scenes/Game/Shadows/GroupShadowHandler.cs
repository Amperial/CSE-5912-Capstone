using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupShadowHandler : MonoBehaviour {
	public GameObject shadowObjectsParent;
	public bool isLightMovable = false;
	// Be explicit with reference to the light in the editor.
	public Light shadowLight;
	public GameObject shadowPlane;
    public GameObject player2D;

	private List<ShadowController> shadowControllers = new List<ShadowController>();

	public void Start() {
		foreach (ShadowConfiguration configuration in shadowObjectsParent.GetComponentsInChildren<ShadowConfiguration>()) {
			if(isLightMovable && configuration.objectType == ShadowConfiguration.ShadowObjectType.Static){
				configuration.objectType = ShadowConfiguration.ShadowObjectType.Dynamic;
			}
			ShadowController controller = ShadowControllerFactory.CreateControllerFromConfiguration (configuration, shadowLight, shadowPlane);
			shadowControllers.Add (controller);
		}
    }

    public void FixedUpdate() {
        foreach (ShadowController shadowController in shadowControllers) {
            shadowController.UpdateShadow();
        }
    }

    void OnEnable() {
        DimensionControl.OnSwitchDimension += OnSwitchDimension;
    }

    void OnDisable() {
        DimensionControl.OnSwitchDimension -= OnSwitchDimension;
    }

    public void OnSwitchDimension(Dimension dimension, Cancellable cancellable) {
        foreach (ShadowController shadowController in shadowControllers) {
            cancellable.PerformCancellable(dimension, shadowController.ConstructShadow, shadowController.DeconstructShadow);

            cancellable.CancelIf(() => dimension == Dimension.TWO_D && !shadowController.IsShadowOkay(player2D));
        }
    }
}
