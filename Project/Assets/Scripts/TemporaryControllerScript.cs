using UnityEngine;

public class TemporaryControllerScript : MonoBehaviour {
    public static bool is2D = false;
    private static GameObject gameSingleton;
    private bool firstFrame = true;

    void Awake() {
        gameSingleton = gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (firstFrame) {
            // Default to 3D mode initially, but wait until everything else is loaded first.
            // There is probably a better way to do this..
            this.BroadcastMessage("SwitchTo3D", new Cancellable(), SendMessageOptions.DontRequireReceiver);
            firstFrame = false;
        }

        if (Input.GetButtonDown("SwapDimension")) {
            SwapDimension();
        }
    }

    public static void SwapDimension() {
        Cancellable cancellable = new Cancellable();
        gameSingleton.BroadcastMessage(is2D ? "SwitchTo3D" : "SwitchTo2D", cancellable, SendMessageOptions.DontRequireReceiver);
        if (!cancellable.IsCancelled()) {
            is2D = !is2D;
        }
    }

}
