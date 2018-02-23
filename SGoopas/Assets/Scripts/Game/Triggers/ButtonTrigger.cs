using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    // Set as 3D player gameobject
    public Collider other;

    // Set as object to manipulate upon trigger
    public GameObject target;

    private ITriggerable triggerable;
    public KeyCode key;
    private bool trigger = false;

    void Start()
    {
        triggerable = target.GetComponent<ITriggerable>();
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && trigger)
        {
            triggerable.Trigger();
        }
    }

    private void OnTriggerStay(Collider collide)
    {
        if (collide.attachedRigidbody == other.attachedRigidbody)
        {
            trigger = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.attachedRigidbody == other.attachedRigidbody)
        {
            trigger = false;
        }
    }
}
