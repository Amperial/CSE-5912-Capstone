using System;
using UnityEngine;

public class ShadowControllerFactory
{
	public static ShadowController CreateControllerFromConfiguration(ShadowConfiguration configuration, bool isLightMovable, Light shadowLight, GameObject shadowPlane) {
		ShadowCaster caster;

		switch (configuration.casterType) {
		case ShadowConfiguration.ShadowCasterType.Polygon:
		default:
			caster = new PolygonShadowCaster(shadowLight, shadowPlane, configuration.gameObject);
			break;
		}

		ShadowController controller;
		if(!isLightMovable){
			switch (configuration.objectType) {
			case ShadowConfiguration.ShadowObjectType.Dynamic:
				controller = new DynamicShadowController (caster, configuration.gameObject);
				break;

			case ShadowConfiguration.ShadowObjectType.Static:
			default:
				controller = new StaticShadowController (caster, configuration.gameObject);
				break;
			}
		}else{
			//Assign dynamic controller for moveable lights
			controller = new DynamicShadowController (caster, configuration.gameObject);
		}

		return controller;
	}
}
	