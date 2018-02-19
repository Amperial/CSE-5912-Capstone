using UnityEngine;

public class DynamicShadowController : ShadowController {

    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

    public override void Start() {
        base.Start();

        rb = gameObject.GetComponent<Rigidbody>();
        linearVelocity = new Vector3();
        angularVelocity = new Vector3();
    }

    public void RemoveShadow() {
        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;

        shadowCaster.DestroyShadow();
    }

    public void RestoreShadow() {
        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;

        shadowCaster.CreateShadow();
    }

    public override void SwitchTo2D(Cancellable cancellable) {
        base.SwitchTo2D(cancellable);
        cancellable.PerformCancellable(RestoreShadow, RemoveShadow);
    }

    public override void SwitchTo3D(Cancellable cancellable) {
        base.SwitchTo3D(cancellable);
        cancellable.PerformCancellable(RemoveShadow, RestoreShadow);
    }

}
