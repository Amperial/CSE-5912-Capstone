using UnityEngine;

public class KeyPressTrigger : MonoBehaviour {

    public KeyCode key;
    public GameObject target;
    private ITriggerable triggerable;

    void Start() {
        triggerable = target.GetComponent<ITriggerable>();
    }

    void Update() {
        if (Input.GetKeyDown(key)) {
            triggerable.Trigger();
        }
    }
}
