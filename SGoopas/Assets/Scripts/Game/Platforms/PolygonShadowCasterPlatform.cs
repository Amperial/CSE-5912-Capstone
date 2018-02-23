using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//New Class, mirorring the original PolygonShadowCaster class, changed to inheriting abstract ShadowCasterPlatform instead of ShadowCaster
public class PolygonShadowCasterPlatform : ShadowCasterPlatform
{
    //Old, look at inheriting instead of adding
    public override void CreateShadow()
    {
        shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));

    }
    /*Identical to CreateShadow with the exceptions listed below.
    Calls to ShadowPolygonHelperPlatoform instead of ShadowPolygonHelper.
    TODO: Add an existing GameObject for the shadow object to the call CreateShadow(), to pass into the update methods.
    */
    public override void UpdateShadow(GameObject shadowObject)
    {
        //Need to insert a GameObject from the CreateShadow call onto the end of this method.
        shadow = ShadowPolygonHelperPlatform.UpdateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)), shadowObject);

    }

}
