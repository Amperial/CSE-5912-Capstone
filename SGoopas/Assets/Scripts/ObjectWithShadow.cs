using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithShadow : MonoBehaviour {
    public Light shadowLight;
    public GameObject shadowPlane;
	private Rigidbody rb;
	private MeshRenderer meshRenderer;
    // Use this for initialization
    void Start()
    {
		SwitchTo2D ();
	}

	private void SwitchTo2D(){
		ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
		meshRenderer = gameObject.GetComponent<MeshRenderer> ();

		if (meshRenderer != null) {
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}

		rb = gameObject.GetComponent<Rigidbody> ();
		if (rb != null) {
			rb.isKinematic = true;
		}
	}
}
