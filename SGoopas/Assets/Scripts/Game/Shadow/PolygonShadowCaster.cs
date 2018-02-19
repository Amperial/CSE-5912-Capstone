using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {
	public PolygonShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) : base(shadowLight, shadowPlane, shadowObject) {}

    public override void CreateShadow()
    {
        if(shadowLight.type == LightType.Point){
            shadow = ShadowPolygonHelper.CreateShadowGameObject(shadowObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
        }
        else{
            shadow = ShadowPolygonHelper.CreateDirectionalShadowGameObject(shadowObject, shadowLight.gameObject.transform.forward, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
        }
    }
}
