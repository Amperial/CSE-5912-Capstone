using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithShadow : MonoBehaviour {
    public Light shadowLight;
    public GameObject shadowPlane;

    // Use this for initialization
    void Start()
    {
        ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
    }
}
