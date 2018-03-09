using System;
using UnityEngine;

public class ShadowControllerFactory
{
	public static ShadowController CreateControllerFromConfiguration(ShadowConfiguration configuration, Light shadowLight, GameObject shadowPlane) {
		ShadowCaster caster;

		switch (configuration.casterType) {
		case ShadowConfiguration.ShadowCasterType.Polygon:
		default:
			caster = new PolygonShadowCaster(shadowLight, shadowPlane, configuration.gameObject);
			break;
		}

		ShadowController controller;

		switch (configuration.objectType) {
		case ShadowConfiguration.ShadowObjectType.Dynamic:
			controller = new DynamicShadowController (caster, configuration.gameObject);
			break;

		case ShadowConfiguration.ShadowObjectType.Realtime:
			controller = new RealtimeShadowController (caster, configuration.gameObject);
			break;

		case ShadowConfiguration.ShadowObjectType.Static:
		default:
			controller = new StaticShadowController (caster, configuration.gameObject);
			break;
		}

		return controller;
	}
}
	