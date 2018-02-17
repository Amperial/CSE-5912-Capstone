using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowController {
	
    protected GameObject gameObject;
    protected ShadowCaster shadowCaster;

	private MeshRenderer meshRenderer;

	public ShadowController(ShadowCaster caster, GameObject gameObject) {
		shadowCaster = caster;
		this.gameObject = gameObject;
		this.meshRenderer = this.gameObject.GetComponent<MeshRenderer> ();
	}

	/*
	 * Sets up the initial parameters for the controller, called from the parent in the hierarchy.
	 */
	public virtual void ConfigureWithLightParams(Light shadowLight, GameObject shadowPlane) {
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
