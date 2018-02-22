﻿using System.Collections;
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

    public virtual bool IsShadowOkay(GameObject player)
    {
        return true;
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
