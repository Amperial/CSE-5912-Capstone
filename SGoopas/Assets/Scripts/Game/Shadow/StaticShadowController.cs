using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShadowController : ShadowController {
	public StaticShadowController(ShadowCaster caster, GameObject gameObject) : base(caster, gameObject) {}

    public override void ConstructShadow()
    {
		shadowCaster.ShowShadow();
    }

    public override bool IsShadowOkay(GameObject player)
    {
        return true;
    }

    public override void DeconstructShadow()
    {
        shadowCaster.HideShadow();
    }

	public override void UpdateShadow() {
		// No-op, static shadows don't need to be updated in real-time.
	}
}
