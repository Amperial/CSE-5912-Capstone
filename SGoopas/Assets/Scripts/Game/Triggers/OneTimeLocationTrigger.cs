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
        if (!triggered && other.gameObject.Equals(MainObjectContainer.Instance.Player3D))
        {
            triggerable.Trigger();
            triggered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.gameObject.Equals(MainObjectContainer.Instance.Player2D))
        {
            triggerable.Trigger();
            triggered = true;
        }
    }
}
