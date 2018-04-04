using System;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour, IInteractable, ITriggerable
{
	private ITriggerable triggerable;
	private SwitchContainer container;
	private int indexInParent;
	private Shader original;
	public Shader activeShader;
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
			if (((BinaryPuzzle)container).NotifyTriggerStateChange (indexInParent)) {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x, .6f, .6f); 
			} else {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x, .3f, .3f); 
			}
		}
	}

	public Shader GetOriginalShader(){
		return original;
	}
}


