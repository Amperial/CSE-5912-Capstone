using System;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour, IInteractable, ITriggerable
{
	private ITriggerable triggerable;
	private SwitchContainer container;
	private int indexInParent;
	private Shader original;

	public void Awake()
	{
		original = gameObject.GetComponent<Renderer>().material.shader;
		if (gameObject.transform.parent != null) {
			GameObject parent = gameObject.transform.parent.gameObject;
			if (parent.transform.parent != null) {
				GameObject grandParent = parent.transform.parent.gameObject;
				container = grandParent.GetComponent<SwitchContainer>();
			}
		}
	}

	void Start() {
	}

	public void SetIndexInParent(int i){
		indexInParent = i;
	}

	public void Interact() {
		Trigger();
	}

	public void Trigger() {
		//triggerable.Trigger();
		if (container != null && container is BinaryPuzzle) {
			((BinaryPuzzle)container).NotifyTriggerStateChange (indexInParent);
		}
	}

	public Shader GetOriginalShader(){
		return original;
	}
}


