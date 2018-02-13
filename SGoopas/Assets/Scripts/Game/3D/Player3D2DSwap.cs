using UnityEngine;

public class Player3D2DSwap : MonoBehaviour {

    private Player3DControl controller;
    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

    void Start() {
        controller = this.gameObject.GetComponent<Player3DControl>();
        controller.enabled = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        linearVelocity = new Vector3();
        angularVelocity = new Vector3();
    }

    public void Enable3DPlayer() {
        controller.enabled = true;
        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
    }

    public void Disable3DPlayer() {
        controller.enabled = false;

        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;
    }

    public void SwitchTo2D(Cancellable cancellable) {
        if (!cancellable.IsCancelled()) {
            Disable3DPlayer();
            cancellable.IfCancelled(Enable3DPlayer);
        }
    }

    public void SwitchTo3D(Cancellable cancellable) {
        if (!cancellable.IsCancelled()) {
            Enable3DPlayer();
            cancellable.IfCancelled(Disable3DPlayer);
        }
    }

}
