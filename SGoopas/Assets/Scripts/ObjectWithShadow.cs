using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithShadow : MonoBehaviour {
    public Light shadowLight;
    public GameObject shadowPlane;
	private Rigidbody rb;
	private MeshRenderer meshRenderer;
	private GameObject shadow;
    // Use this for initialization
    void Start()
    {
		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));

		SwitchTo2D ();
	}

	/*
		This methods makes the object transparent and kinematic
	*/
	private void SwitchTo2D(){

		if (meshRenderer != null) {
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}

		if (rb != null) {
			rb.isKinematic = true;
		}
	}

	/*
		This methods makes the object visible and non-kinematic.
	*/
	private void SwitchTo3D(){
		if (meshRenderer != null) {
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}

		if (rb != null) {
			rb.isKinematic = false;
		}
	}
}
