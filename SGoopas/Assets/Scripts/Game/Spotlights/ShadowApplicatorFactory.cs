using System;
using UnityEngine;

public class ShadowApplicatorFactory
{
	public static ShadowApplicator CreateApplicatorFromType(Spotlight.ShadowApplicatorType type, GameObject spotlightCollider,  Material shadowMaterial, Material indicatorMaterial) {
		ShadowApplicator applicator;

		switch (type) {
            case Spotlight.ShadowApplicatorType.Physics2D:
                applicator = new Physics2DApplicator(spotlightCollider, shadowMaterial, indicatorMaterial);
                break;
            case Spotlight.ShadowApplicatorType.Hazard:
            default:
			    applicator = new HazardApplicator(spotlightCollider);
			    break;
		}


		return applicator;
	}
}
	