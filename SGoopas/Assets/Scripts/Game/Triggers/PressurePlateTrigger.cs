using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    public GameObject target;
    private ITriggerable triggerable;

    void Start()
    {
        triggerable = target.GetComponent<ITriggerable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerable.Trigger();
}

    void OnTriggerExit(Collider other)
    {
        triggerable.Trigger();
}
}
