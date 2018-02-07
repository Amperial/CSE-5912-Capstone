using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadowController : ShadowController {

    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

    public override void Start()
    {
        base.Start();

        rb = gameObject.GetComponent<Rigidbody>();
        linearVelocity = new Vector3();
        angularVelocity = new Vector3();
    }

    public override void SwitchTo2D()
    {
        base.SwitchTo2D();

        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo3D()
    {
        base.SwitchTo3D();

        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;

        shadowCaster.DestroyShadow();
    }

}
