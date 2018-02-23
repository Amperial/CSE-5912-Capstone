using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShadowController : ShadowController {
	public StaticShadowController(ShadowCaster caster, GameObject gameObject) : base(caster, gameObject) {
		shadowCaster.CreateShadow ();
	}

    public override void ConstructShadow()
    {
		base.ConstructShadow();
        shadowCaster.ShowShadow();
    }

    public override void DeconstructShadow()
    {
        base.DeconstructShadow();

        shadowCaster.HideShadow();
    }
}
