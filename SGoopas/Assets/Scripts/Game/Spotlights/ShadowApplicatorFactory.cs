using System;
using UnityEngine;

public class ShadowApplicatorFactory
{
	public static ShadowApplicator CreateApplicatorFromType(Spotlight.ShadowApplicatorType type, GameObject spotlightCollider, GameObject player2D) {
		ShadowApplicator applicator;

		switch (type) {
            case Spotlight.ShadowApplicatorType.Physics2D:
                applicator = new Physics2DApplicator();
                break;
            case Spotlight.ShadowApplicatorType.Hazard:
            default:
			    applicator = new HazardApplicator(spotlightCollider, player2D);
			    break;
		}


		return applicator;
	}
}
	