using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadowController : ShadowController {
    private Rigidbody rb = null;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

	public DynamicShadowController(ShadowCaster caster, GameObject gameObject) : base(caster, gameObject) {
		rb = gameObject.GetComponent<Rigidbody>();
		linearVelocity = new Vector3();
		angularVelocity = new Vector3();
	}

    public override void ConstructShadow()
    {
        base.ConstructShadow();

        //Freezes rigid body if one exists
        /*
            NOTE: with movable lights, objects without rigidbodies 
            will have a dynamic shadow controller
         */
        if(rb != null){
            linearVelocity = rb.velocity;
            rb.velocity = new Vector3();
            angularVelocity = rb.angularVelocity;
            rb.angularVelocity = new Vector3();

            rb.isKinematic = true;
        }

        shadowCaster.CreateShadow();
    }

    public override void DeconstructShadow()
    {
        base.DeconstructShadow();

        //Unfreezes rigid body if one exists
        if(rb != null){
            rb.isKinematic = false;
            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }
        shadowCaster.DestroyShadow();
    }

}
