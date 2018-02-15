public class StaticShadowController : ShadowController {

    public override void Start() {
        base.Start();

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo2D(Cancellable cancellable) {
        base.SwitchTo2D(cancellable);
    }

    public override void SwitchTo3D(Cancellable cancellable) {
        base.SwitchTo3D(cancellable);
        cancellable.Perform(() => shadowCaster.HideShadow());
        cancellable.OnCancel(() => shadowCaster.ShowShadow());
    }

}
