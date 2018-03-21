using UnityEngine;

public class InteractTrigger : MonoBehaviour, IInteractable, ITriggerable {

    public GameObject target;
    private ITriggerable triggerable;
    public Shader original;
    public void Awake()
    {
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

}
