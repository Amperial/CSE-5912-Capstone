using UnityEngine;

public class BinaryTriggerable : MonoBehaviour, ITriggerable {

    private bool active = false;

    public bool IsActive() {
        return active;
    }

    public virtual void Trigger() {
        active = !active;
    }

}
