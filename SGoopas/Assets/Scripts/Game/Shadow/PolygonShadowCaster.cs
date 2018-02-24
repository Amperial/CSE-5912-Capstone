using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {
	public PolygonShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) : base(shadowLight, shadowPlane, shadowObject) {}

    public override void CreateShadow()
    {
        base.CreateShadow();
        shadow = new GameObject();
        shadow.AddComponent<PolygonCollider2D>();
        ShadowPolygonHelper.CalculateShadowForGameObject(shadow, shadowObject, shadowLight.gameObject.transform.position, shadowLight.type, new Plane(shadowPlane.transform.up.normalized, new Vector3()));
    }

    public override void UpdateShadow() {
        if (shadow != null) {
            ShadowPolygonHelper.CalculateShadowForGameObject(shadow, shadowObject, shadowLight.gameObject.transform.position, shadowLight.type, new Plane(shadowPlane.transform.up.normalized, new Vector3()));
        }
    }
}
