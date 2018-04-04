using PlayerStates;
using UnityEngine;

public class InteractTrigger : ObjInteractableBase, IInteractable, ITriggerable {

    public GameObject target;
    private ITriggerable triggerable;
    public Shader original;
    public override void Awake()
    {
        base.Awake();
        original = gameObject.GetComponent<Renderer>().material.shader;
    }

    void Start() {
        triggerable = target.GetComponent<ITriggerable>();
    }

    public void Interact() {
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
