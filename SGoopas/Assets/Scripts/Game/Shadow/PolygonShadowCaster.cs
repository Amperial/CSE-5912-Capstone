using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {
	public PolygonShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) : base(shadowLight, shadowPlane, shadowObject) {}

    public override void CreateShadow()
    {
        shadow = ShadowPolygonHelper.CreateShadowGameObject(shadowObject, shadowLight.gameObject.transform.position, shadowLight.type, new Plane(shadowPlane.transform.up.normalized, new Vector3()));
    }

    public override void UpdateShadow() {
        if (shadow != null) {
            ShadowPolygonHelper.UpdateShadowGameObject(shadowObject, shadowLight.gameObject.transform.position, shadowLight.type, new Plane(shadowPlane.transform.up.normalized, new Vector3()), shadow);
        }
    }
}
