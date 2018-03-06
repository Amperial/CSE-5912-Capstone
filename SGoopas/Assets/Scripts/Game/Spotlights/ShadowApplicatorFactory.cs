using System;
using UnityEngine;

public class ShadowApplicatorFactory
{
	public static ShadowApplicator CreateApplicatorFromType(Spotlight.ShadowApplicatorType type, GameObject spotlightCollider) {
		ShadowApplicator applicator;

		switch (type) {
            case Spotlight.ShadowApplicatorType.Physics2D:
                break;
            case Spotlight.ShadowApplicatorType.Hazard:
            default:
			    applicator = new PolygonShadowCaster(shadowLight, shadowPlane, configuration.gameObject);
			    break;
		}


		return applicator;
	}
}
	