using UnityEngine;

public class DoubleTrigger : MonoBehaviour, IInteractable, ITriggerable
{

    public GameObject target1;
    public GameObject target2;
    private ITriggerable triggerable1;
    private ITriggerable triggerable2;
    public Shader original;
    public void Awake()
    {
        original = gameObject.GetComponent<Renderer>().material.shader;
    }

    void Start()
    {
        triggerable1 = target1.GetComponent<ITriggerable>();
        triggerable2 = target2.GetComponent<ITriggerable>();
    }

    public void Interact()
    {
        Trigger();
    }

    public void Trigger()
    {
        triggerable1.Trigger();
        triggerable2.Trigger();
    }

}
