using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIleftOff : MonoBehaviour {

/*
 * Error with StaticShadowControllerPlatform pulling form the shadowCaster instead of shadowCasterPlatform. Once that's done, you have to still have to implement the shadowObject passing from the first
 * call in StaticShadowControllerPlatform all the way through the updating in said class. The object is passed from that call all the way through PolygonShadowCasterPlatform's UpdateShadow() call to the
 * ShadowPolygonHelperPlatform, multiple call in that class should be marked. Once it's passed through there, the Object should be update, and redrawn with the new coordinates. You have to save the 
 * shadowObject so it can be passed back into the method. Feel free to use the normal CreateShadow() call to make the shadow at first. You might have to change the inheritance of the PolygonShadowCasterPlatform
 * so it still has access to CreateShadow call. 
 * 
 * Query: how much of this should simply be placed into the existing shadow code????
 */
}
