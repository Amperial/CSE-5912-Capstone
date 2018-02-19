using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowController : MonoBehaviour {

    protected MeshRenderer meshRenderer;
    protected ShadowCaster shadowCaster;

	/*
	 * Sets up the initial parameters for the controller, called from the parent in the hierarchy.
	 * All initialization code should go in here instead of start. 
	 */
	public virtual void ConfigureWithLightParams(Light shadowLight, GameObject shadowPlane) {
		meshRenderer = gameObject.GetComponent<MeshRenderer>();
		shadowCaster = gameObject.GetComponent<ShadowCaster>();
		shadowCaster.ConfigureWithLightParams (shadowLight, shadowPlane);
	}

	public virtual void ConstructShadow()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

	public virtual void DeconstructShadow()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
