using UnityEngine;

public class Player3D2DSwap : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 linearVelocity;
    private Vector3 angularVelocity;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        linearVelocity = new Vector3();
        angularVelocity = new Vector3();
    }

    public void Enable3DPlayer() {
        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
    }

    public void Disable3DPlayer() {
        linearVelocity = rb.velocity;
        rb.velocity = new Vector3();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = new Vector3();

        rb.isKinematic = true;
    }

    public void SwitchTo2D(Cancellable cancellable) {
        cancellable.PerformCancellable(Disable3DPlayer, Enable3DPlayer);
    }

    public void SwitchTo3D(Cancellable cancellable) {
        cancellable.PerformCancellable(Enable3DPlayer, Disable3DPlayer);
    }

}
