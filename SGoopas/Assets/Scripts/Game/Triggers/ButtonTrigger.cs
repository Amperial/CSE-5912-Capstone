using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    //public Collider other;
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

    private void OnTriggerStay(Collider other)
    {
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }
}
