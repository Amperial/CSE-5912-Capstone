using System;
using UnityEngine;

public class ShadowControllerFactory
{
	public static ShadowController CreateControllerFromConfiguration(ShadowConfiguration configuration) {
		ShadowCaster caster;

		switch (configuration.casterType) {
		case ShadowConfiguration.ShadowCasterType.Polygon:
		default:
			caster = new PolygonShadowCaster ();
			break;
		}

		ShadowController controller;
		switch (configuration.objectType) {
		case ShadowConfiguration.ShadowObjectType.Dynamic:
			controller = new DynamicShadowController (caster, configuration.gameObject);
			break;

		case ShadowConfiguration.ShadowObjectType.Static:
		default:
			controller = new StaticShadowController (caster, configuration.gameObject);
			break;
		}

		return controller;
	}
}
	