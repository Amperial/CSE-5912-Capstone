using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {

	public PolygonShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) : base(shadowLight, shadowPlane, shadowObject) {}

    public override void CreateShadow()
    {
        shadow = ShadowPolygonHelper.CreateShadowGameObject(shadowObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
    }

}
