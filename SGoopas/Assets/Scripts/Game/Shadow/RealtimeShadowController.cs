using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeShadowController : ShadowController {
	public RealtimeShadowController (ShadowCaster caster, GameObject gameObject) : base (caster, gameObject) {}

	public override void ConstructShadow()
	{
		base.ConstructShadow();
		shadowCaster.CreateShadow();
	}

	public override void DeconstructShadow()
	{
		base.DeconstructShadow();
		shadowCaster.DestroyShadow();
	}

	public override void UpdateShadow() 
    {
		shadowCaster.UpdateShadow ();
	}
}
