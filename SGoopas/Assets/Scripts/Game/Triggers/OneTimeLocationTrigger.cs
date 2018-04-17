using System.Collections.Generic;
using UnityEngine;

public class OneTimeLocationTrigger : MonoBehaviour
{
    public GameObject target;
    private ITriggerable triggerable;
    private bool triggered = false;

    void Start()
    {
        triggerable = target.GetComponent<ITriggerable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggerable.Trigger();
            triggered = true;
        }
    }
}
