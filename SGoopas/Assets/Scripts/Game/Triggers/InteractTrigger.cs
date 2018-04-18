using PlayerStates;
using UnityEngine;

public class InteractTrigger : ObjInteractableBase, IInteractable, ITriggerable {

    public GameObject target;
    private ITriggerable triggerable;

    private SwitchContainer container;
    private int indexInParent;

    void Start() {
        triggerable = target.GetComponent<ITriggerable>();
    }

    public virtual void Interact() {
        Trigger();
    }

    public void Trigger() {
        triggerable.Trigger();
    }

    public override IPlayerState PlayerBeganInteraction(BasePlayerState currentState)
    {
        Interact();
        return null;
    }

    public override void InteractionEnded()
    {
        // No-op.
    }

    public override bool IsPlayerAbleToInteract(GameObject player)
    {
        return true;
    }
}
