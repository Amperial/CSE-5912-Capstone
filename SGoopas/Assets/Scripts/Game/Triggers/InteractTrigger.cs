using UnityEngine;

public class InteractTrigger : MonoBehaviour, IInteractable, ITriggerable {

    public ITriggerable target;

    public void Interact() {
        Trigger();
    }

    public void Trigger() {
        target.Trigger();
    }

}
