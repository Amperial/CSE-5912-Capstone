using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShadowController : ShadowController {

    public override void Start()
    {
        base.Start();

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo2D()
    {
        base.SwitchTo2D();

        shadowCaster.ShowShadow();
    }

    public override void SwitchTo3D()
    {
        base.SwitchTo3D();

        shadowCaster.HideShadow();
    }

}
