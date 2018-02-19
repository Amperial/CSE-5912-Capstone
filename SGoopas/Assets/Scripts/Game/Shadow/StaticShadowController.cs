public class StaticShadowController : ShadowController {

    public override void Start() {
        base.Start();

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo2D(Cancellable cancellable) {
        base.SwitchTo2D(cancellable);
        cancellable.PerformCancellable(() => shadowCaster.ShowShadow(), () => shadowCaster.HideShadow());
    }

    public override void SwitchTo3D(Cancellable cancellable) {
        base.SwitchTo3D(cancellable);
        cancellable.PerformCancellable(() => shadowCaster.HideShadow(), () => shadowCaster.ShowShadow());
    }

}
