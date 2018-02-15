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
    // Elapsed time moving between positions used by coroutine
    private float elapsed = 0f;

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
    }

    public override void Trigger() {
        base.Trigger();
        
        // Check if coroutine is still running or stopped for some reason
        if (elapsed > 0f) {
            // Make sure coroutine is stopped
            StopAllCoroutines();
            // Calculate elapsed time for new coroutine
            elapsed = (moveTime - elapsed);
        }

        // Start coroutine to step to position
        StartCoroutine(StepToPosition());
    }

}
