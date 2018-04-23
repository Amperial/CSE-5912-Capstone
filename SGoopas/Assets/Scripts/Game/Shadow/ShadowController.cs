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
	}

	public abstract void UpdateShadow ();

    public abstract bool IsShadowOkay(GameObject player);

    public abstract void ConstructShadow();

    public abstract void DeconstructShadow();
}
