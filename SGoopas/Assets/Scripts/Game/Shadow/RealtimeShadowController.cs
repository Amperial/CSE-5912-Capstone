using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeShadowController : ShadowController {
	public RealtimeShadowController (ShadowCaster caster, GameObject gameObject) : base (caster, gameObject) {}

	public override void ConstructShadow()
	{
		shadowCaster.CreateShadow();
	}

    public override bool IsShadowOkay(GameObject player)
    {
        GameObject shadow = shadowCaster.GetShadow();
        Collider2D shadowCollider = shadow.GetComponent<Collider2D>();
        Vector3 playerPosition = player.transform.position;
        return !shadowCollider.OverlapPoint(new Vector2(playerPosition.x, playerPosition.y));
    }

	public override void DeconstructShadow()
	{
		shadowCaster.DestroyShadow();
	}

	public override void UpdateShadow() 
    {
		shadowCaster.UpdateShadow ();
	}
}
