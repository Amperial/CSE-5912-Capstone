using PlayerStates;
using UnityEngine;

/*
 * Will cause a button to scale when you press it.
 */
public class ButtonInteractTrigger : InteractTrigger {
    private Vector3 originalScale;
    private bool triggerOn = false;

    public override void Interact() {
        base.Interact();
        triggerOn = !triggerOn;
        MasterMonoBehaviour.Instance.Click();

        if (triggerOn)
        {
            originalScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1.5f * gameObject.transform.localScale.y, 1.5f * gameObject.transform.localScale.z);
        } else {
            gameObject.transform.localScale = originalScale;
        }
    }
}
