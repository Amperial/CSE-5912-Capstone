using System.Collections;
using UnityEngine;

public class BinaryPosition : BinaryTriggerable {

    // Vectors to keep track of initial and active positions
    private Vector3 initialPosition;
    private Vector3 initialRotation;
    public Vector3 activePosition;
    public Vector3 activeRotation;

    // Total time to move between positions
    public float moveTime = 1f;

    // Reference to coroutine
    private Coroutine coroutine;
    // Elapsed time moving between positions used by coroutine
    private float elapsed = 0f;
    private bool IsMoving {
        get { return elapsed > 0f; }
    }

    public void Start() {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation.eulerAngles;
    }

    IEnumerator StepToPosition() {
        // Calculate delta position and rotation for movement to position
        Vector3 deltaPosition = (initialPosition - activePosition) * Time.fixedDeltaTime / moveTime;
        Vector3 deltaRotation = (initialRotation - activeRotation) * Time.fixedDeltaTime / moveTime;
        if (IsActive()) {
            deltaPosition *= -1;
            deltaRotation *= -1;
        }
        // Move transform by delta position and rotation for specified time
        while (elapsed < moveTime) {
            transform.position += deltaPosition;
            transform.Rotate(deltaRotation);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        // Set transform position and rotation to exact final values
        transform.localPosition = IsActive() ? activePosition : initialPosition;
        transform.localRotation = Quaternion.Euler(IsActive() ? activeRotation : initialRotation);
        elapsed = 0f;

        // Movement is done, return to 2D

        // Will be re-implemented with event-based switch
        //if (returnTo2D) {
        //    TemporaryControllerScript.SwapDimension();
        //    returnTo2D = false;
        //}
    }

    public override void Trigger() {
        base.Trigger();

        // Switch to 3D and return to 2D once movement is done.
        // TODO: Replace with general system to focus on world changes & remove player control

        // Will be re-implemented with event-based switch
        //if (TemporaryControllerScript.is2D) {
        //    TemporaryControllerScript.SwapDimension();
        //    returnTo2D = true;
        //}

        // Check if coroutine is still running or stopped for some reason
        if (IsMoving) {
            // Make sure coroutine is stopped
            StopCoroutine(coroutine);
            // Calculate elapsed time for new coroutine
            elapsed = (moveTime - elapsed);
        }

        // Start coroutine to step to position
        coroutine = StartCoroutine(StepToPosition());
    }

    public void SwitchTo2D(Cancellable cancellable) {
        // Prevent switching to 2D while moving between positions
        if (IsMoving) {
            cancellable.Cancel();
        }
    }

}
