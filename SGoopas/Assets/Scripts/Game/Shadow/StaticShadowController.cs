using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShadowController : ShadowController {

    public override void Start()
    {
        base.Start();

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo2D(Cancellable cancellable)
    {
        if (!cancellable.IsCancelled())
        {
            base.SwitchTo2D(cancellable);

            shadowCaster.ShowShadow();
            cancellable.IfCancelled(() => shadowCaster.HideShadow());
        }
    }

    public override void SwitchTo3D(Cancellable cancellable)
    {
        if (!cancellable.IsCancelled())
        {
            base.SwitchTo3D(cancellable);

            shadowCaster.HideShadow();
            cancellable.IfCancelled(() => shadowCaster.ShowShadow());
        }
    }

}
