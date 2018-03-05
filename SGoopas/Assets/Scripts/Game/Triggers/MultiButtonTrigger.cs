using UnityEngine;

public class MultiButtonTrigger : MonoBehaviour
{

    // Works in conjunction with MultiPositionsButton

    public KeyCode key;
    public GameObject target;
    private ITriggerable triggerable;
    private bool trigger = false;

    // Set as 3D player collider
    public Collider collide;

    public int index;

    void Start()
    {
        triggerable = target.GetComponent<ITriggerable>();
    }

    void Update()
    {

        // Eventually replacce with action button
        if (Input.GetKeyDown(key) && trigger)
        {
            triggerable.Trigger();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody == collide.attachedRigidbody)
        {
            trigger = true;
            Component[] script = GameObject.Find("Platforms").GetComponentsInChildren<MultiPositionsButton>();

            foreach (MultiPositionsButton position in script)
            {
                MultiPositionsButton.index = index;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == collide.attachedRigidbody)
        {
            trigger = false;
        }
    }
}
