using System.Collections.Generic;
using UnityEngine;

public class MultipleTriggers: MonoBehaviour, IInteractable, ITriggerable
{

    public List<GameObject> targets = new List<GameObject>();
    private List<ITriggerable> triggerable = new List<ITriggerable>();
    public Shader original;
    private ITriggerable newSlot;
    public void Awake()
    {
        original = gameObject.GetComponent<Renderer>().material.shader;
    }

    void Start()
    {
        
        for(int i = 0; i < targets.Count; i++)
        {
            triggerable.Add(newSlot);
            triggerable[i] = targets[i].GetComponent<ITriggerable>();
        }
        
    }

    public void Interact()
    {
        Trigger();
    }

    public void Trigger()
    {
        for (int i = 0; i < triggerable.Count; i++)
        {
            triggerable[i].Trigger();
        }
    }

}
