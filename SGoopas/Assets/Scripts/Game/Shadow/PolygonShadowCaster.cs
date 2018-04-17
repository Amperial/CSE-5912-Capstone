using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {
	public PolygonShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) : base(shadowLight, shadowPlane, shadowObject) {}

    public override void CreateShadow()
    {
        base.CreateShadow();
        shadow = new GameObject();
        shadow.AddComponent<PolygonCollider2D>();
        ShadowPolygonHelper.CalculateShadowForGameObject(shadow, shadowObject, shadowLight, new Plane(shadowPlane.transform.up.normalized, new Vector3()));
        Rigidbody rb = shadowObject.GetComponent<Rigidbody>();
        if (rb) {
            Rigidbody2D rb2d = shadow.AddComponent<Rigidbody2D>();
            rb2d.bodyType = RigidbodyType2D.Static;
            rb2d.mass = rb.mass;
        }
    }

    public override void UpdateShadow() {
        if (shadow != null) {
            ShadowPolygonHelper.CalculateShadowForGameObject(shadow, shadowObject, shadowLight, new Plane(shadowPlane.transform.up.normalized, new Vector3()));
        }
    }
}
