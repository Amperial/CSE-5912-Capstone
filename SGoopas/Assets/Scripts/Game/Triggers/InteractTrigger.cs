using UnityEngine;

public class InteractTrigger : MonoBehaviour, IInteractable, ITriggerable {

    public GameObject target;
    private ITriggerable triggerable;
    private Shader original;
	private SwitchContainer container;
	private int indexInParent;

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

	public Shader GetOriginalShader(){
		return original;
	}
}
