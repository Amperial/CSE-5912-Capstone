using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {

    public override void CreateShadow(GameObject gameObject)
    {
        shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
    }

}
