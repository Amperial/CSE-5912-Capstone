using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShadowController : ShadowController {
	public override void ConfigureWithLightParams(Light shadowLight, GameObject shadowPlane) { 
		// Wait until we get the light params from above to construct the shadow.
		base.ConfigureWithLightParams (shadowLight, shadowPlane);
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
