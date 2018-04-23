using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupShadowHandler : MonoBehaviour {
	public GameObject shadowObjectsParent;
	public bool isLightMovable = false;
	// Be explicit with reference to the light in the editor.
	public Light shadowLight;
    public bool hiddenObjects = false;
	private List<ShadowController> shadowControllers = new List<ShadowController>();

	public void Start() {
		foreach (ShadowConfiguration configuration in shadowObjectsParent.GetComponentsInChildren<ShadowConfiguration>()) {
			if(isLightMovable && configuration.objectType == ShadowConfiguration.ShadowObjectType.Static){
				configuration.objectType = ShadowConfiguration.ShadowObjectType.Dynamic;
			}
			ShadowController controller = ShadowControllerFactory.CreateControllerFromConfiguration (configuration, shadowLight, MainObjectContainer.Instance.ShadowPlane);
			shadowControllers.Add (controller);
		}
	}

	public void SwitchTo2D(Cancellable cancellable) {
		foreach (ShadowController shadowController in shadowControllers) {
            cancellable.PerformCancellable(shadowController.ConstructShadow, shadowController.DeconstructShadow);
            cancellable.PerformCancellable(HideShadowObjects, ShowShadowObjects);

            if (!cancellable.IsCancelled && !shadowController.IsShadowOkay(MainObjectContainer.Instance.Player2D))
                cancellable.Cancel();
		}
	}

    private void HideShadowObjects() {
        foreach (MeshRenderer meshRenderer in shadowObjectsParent.GetComponentsInChildren<MeshRenderer>())
        {
            if (meshRenderer.gameObject.GetComponent<ShadowConfiguration>() != null)
            {
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            } else {
                meshRenderer.enabled = false;
            }
        }
    }

    private void ShowShadowObjects() {
        foreach (MeshRenderer meshRenderer in shadowObjectsParent.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.enabled = true;
            if (!hiddenObjects)
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            else
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }

	public void SwitchTo3D(Cancellable cancellable) { 
		foreach (ShadowController shadowController in shadowControllers) {
                    cancellable.PerformCancellable(shadowController.DeconstructShadow, shadowController.ConstructShadow);
                    cancellable.PerformCancellable(ShowShadowObjects, HideShadowObjects);
           
        }
	}

	public void FixedUpdate() {
		foreach (ShadowController shadowController in shadowControllers) {
			shadowController.UpdateShadow();
		}
	}
}
