using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadowController : ShadowController {
    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

	public DynamicShadowController(ShadowCaster caster, GameObject gameObject) : base(caster, gameObject) {
		rb = gameObject.GetComponent<Rigidbody>();
		linearVelocity = new Vector3();
		angularVelocity = new Vector3();
	}

    public override bool IsShadowOkay(GameObject player)
    {
        GameObject shadow = shadowCaster.GetShadow();
        Collider2D shadowCollider = shadow.GetComponent<Collider2D>();
        Vector3 playerPosition = player.transform.position;
        return !shadowCollider.OverlapPoint(new Vector2(playerPosition.x, playerPosition.y));
    }

    public override void ConstructShadow()
    {
        base.ConstructShadow();

        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;

        shadowCaster.CreateShadow();
    }

    public override void DeconstructShadow()
    {
        base.DeconstructShadow();

        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;

        shadowCaster.DestroyShadow();
    }

}
