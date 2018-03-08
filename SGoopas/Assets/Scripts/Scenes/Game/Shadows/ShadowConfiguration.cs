using UnityEngine;
using System.Collections;

/*
 * Set of enums representing a shadow configuration, including a controller and a caster. 
 */
public class ShadowConfiguration : MonoBehaviour
{
	public enum ShadowObjectType { Static, Dynamic, Realtime };
	public enum ShadowCasterType { Polygon };

	public ShadowObjectType objectType;
	public ShadowCasterType casterType;
}
