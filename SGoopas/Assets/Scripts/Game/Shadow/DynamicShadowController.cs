using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShadowController : ShadowController {

    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;
    private Coroutine coroutine;
    private Transform player;

    public override void Start() {
        base.Start();

        rb = gameObject.GetComponent<Rigidbody>();
        linearVelocity = new Vector3();
        angularVelocity = new Vector3();

        player = GameObject.Find("Player").GetComponent<Transform>();
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

    private void CheckShadowGeneration(Cancellable cancellable)
    {
        GameObject shadow = shadowCaster.GetShadow();
        Collider2D shadowCollider = shadow.GetComponent<Collider2D>();
        Vector3 playerPosition = player.position;
        if (shadowCollider.OverlapPoint(new Vector2(playerPosition.x, playerPosition.y)))
            cancellable.Cancel();
    }

    public override void SwitchTo2D(Cancellable cancellable) {
        base.SwitchTo2D(cancellable);
        cancellable.PerformCancellable(RestoreShadow, RemoveShadow);
        if (!cancellable.IsCancelled)
        {
            CheckShadowGeneration(cancellable);
        }
    }

    public override void SwitchTo3D(Cancellable cancellable) {
        base.SwitchTo3D(cancellable);
        cancellable.PerformCancellable(RemoveShadow, RestoreShadow);
    }

}
