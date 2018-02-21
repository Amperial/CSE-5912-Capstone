public class StaticShadowControllerPlatform : ShadowController
{

    public override void Start()
    {
        base.Start();

        shadowCaster.CreateShadow();
    }

    private void FixedUpdate()
    {
        shadowCaster.DestroyShadow();
        shadowCaster.CreateShadow();
    }

}
