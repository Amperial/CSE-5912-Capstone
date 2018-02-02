using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithShadow : MonoBehaviour {
    public Light shadowLight;

    // Use this for initialization
    void Start()
    {
        ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(new Vector3(0, 0, 1).normalized, new Vector3(0, 0, 0)));
    }
	
	// Update is called once per frame
	void Update () {
        ShadowPolygonHelper.GetPointLightShadow(shadowLight.gameObject.transform.position, gameObject, new Plane(new Vector3(0, 0, 1).normalized, new Vector3(0, 0, 0)));
	}
}
