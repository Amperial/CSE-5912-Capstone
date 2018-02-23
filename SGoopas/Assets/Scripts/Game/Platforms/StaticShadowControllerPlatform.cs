using UnityEngine;

public class StaticShadowControllerPlatform : ShadowControllerPlatform
{
    private GameObject shadowObject;
    public override void Start()
    {
        base.Start();

        shadowCasterPlatform.CreateShadow();
        shadowObject = shadowCasterPlatform.GetShadow();
    }

    private void FixedUpdate()
    {
        //shadowCaster.DestroyShadow();

        //update the points on the shadow
        shadowCasterPlatform.UpdateShadow(shadowObject);
        //retrieve new shadow
        shadowObject = shadowCasterPlatform.GetShadow();
    }

}
