using UnityEngine;

public abstract class ShadowController : MonoBehaviour {

    protected MeshRenderer meshRenderer;
    protected ShadowCaster shadowCaster;

    public virtual void Start() {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        shadowCaster = gameObject.GetComponent<ShadowCaster>();
    }

    public void ShowObject() {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    public void HideObject() {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

    public virtual void SwitchTo2D(Cancellable cancellable) {
        cancellable.Perform(HideObject);
        cancellable.OnCancel(ShowObject);
    }

    public virtual void SwitchTo3D(Cancellable cancellable) {
        cancellable.Perform(ShowObject);
        cancellable.OnCancel(HideObject);
    }

}
