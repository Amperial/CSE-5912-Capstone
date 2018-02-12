using UnityEngine;

public class BinaryPosition : BinaryTriggerable {

    public float moveTime = 1f;

    public Vector3 activePosition;
    public Vector3 activeRotation;

    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Vector3 deltaPosition;
    private Vector3 deltaRotation;

    private bool moving = false;
    private float timePassed = 0f;

	public void Start () {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation.eulerAngles;

        deltaPosition = (activePosition - initialPosition) / moveTime;
        deltaRotation = (activeRotation - initialRotation) / moveTime;
	}

	public void Update () {
        // Check if object is still moving toward target position
		if (!moving)
        {
            return;
        }
        
        // Check which state object is moving towards
        if (IsActive())
        {
            // Check if object has been moving long enough to reach target
            if (timePassed < moveTime)
            {
                // Move object by constant speed towards target
                transform.position += deltaPosition * Time.deltaTime;
                transform.Rotate(deltaRotation * Time.deltaTime);
                timePassed += Time.deltaTime;
            } else
            {
                // Finished moving, set object's position and rotation exactly to the target
                transform.localPosition = activePosition;
                transform.localRotation = Quaternion.Euler(activeRotation);
                timePassed = moveTime;
                moving = false;
            }
        } else
        {
            // Same code except in reverse and moving to initial position
            if (timePassed > 0)
            {
                transform.position -= deltaPosition * Time.deltaTime;
                transform.Rotate(-deltaRotation * Time.deltaTime);
                timePassed -= Time.deltaTime;
            } else
            {
                transform.localPosition = initialPosition;
                transform.localRotation = Quaternion.Euler(initialRotation);
                timePassed = 0;
                moving = false;
            }
        }
	}

    public override void Trigger()
    {
        base.Trigger();

        moving = true;
    }

}
