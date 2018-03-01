using UnityEngine;

public class Player2D3DSwap : MonoBehaviour {
    
    private Rigidbody2D rb;
    private Vector2 linearVelocity;
    private float angularVelocity;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        linearVelocity = new Vector2();
        angularVelocity = 0.0f;
    }

    public void Enable2DPlayer() {
        rb.isKinematic = false;

        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
    }

    public void Disable2DPlayer() {

        linearVelocity = rb.velocity;
        rb.velocity = new Vector2();
        angularVelocity = rb.angularVelocity;
        rb.angularVelocity = 0.0f;

        rb.isKinematic = true;
    }

    public void SwitchTo2D(Cancellable cancellable) {
        cancellable.PerformCancellable(Enable2DPlayer, Disable2DPlayer);
    }

    public void SwitchTo3D(Cancellable cancellable) {
        cancellable.PerformCancellable(Disable2DPlayer, Enable2DPlayer);
    }

}
