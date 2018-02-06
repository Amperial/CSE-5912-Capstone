using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithShadow : MonoBehaviour {
    public Light shadowLight;
    public GameObject shadowPlane;
	private Rigidbody rb;
	private MeshRenderer meshRenderer;
	private GameObject shadow;

    private bool objectIsStatic = false;

    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

    // Use this for initialization
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            objectIsStatic = true;
        }
        if (objectIsStatic) { 
            shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
            shadow.SetActive(false);
        } else
        {
            linearVelocity = new Vector3();
            angularVelocity = new Vector3();
        }

	}

	/*
		This methods makes the object transparent and calls either the static or dynamic implementation, depending on the object
	*/
	public void SwitchTo2D(){

        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        if (objectIsStatic)
        {
            SwitchTo2DStatic();
        } else
        {
            SwitchTo2DDynamic();
        }
	}

    /*
		This method makes the object kinematic and generates a shadow
	*/
    private void SwitchTo2DDynamic()
    {
        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;

        shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, shadowLight.gameObject.transform.position, new Plane(shadowPlane.transform.up.normalized, new Vector3(0, 0, 0)));
    }
    /*
		This makes the object's shadow active in the hiarchy.
	*/
    private void SwitchTo2DStatic()
    {
        shadow.SetActive(true);
    }

	/*
		This method makes the object visible and calls either the static or dynamic implementation, depending on the object
	*/
	public void SwitchTo3D(){
		if (meshRenderer != null) {
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}

        if (objectIsStatic)
        {
            SwitchTo3DStatic();
        }
        else
        {
            SwitchTo3DDynamic();
        }
    }
    /*
		This method the rigidbody non-kinematic and destroy's the objects shadow
	*/
    private void SwitchTo3DDynamic()
    {
        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;

        Destroy(shadow);
    }
    /*
		This method deactivates the object's shadow in the hiarchy
	*/
    private void SwitchTo3DStatic()
    {
        shadow.SetActive(false);
    }
}
