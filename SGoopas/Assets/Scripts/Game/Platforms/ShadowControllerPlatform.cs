using UnityEngine;

public abstract class ShadowControllerPlatform : MonoBehaviour
{

    protected MeshRenderer meshRenderer;
    protected ShadowCasterPlatform shadowCasterPlatform;

    public virtual void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        shadowCasterPlatform = gameObject.GetComponent<ShadowCasterPlatform>();
    }

    public void ShowObject()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    public void HideObject()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

    public virtual void SwitchTo2D(Cancellable cancellable)
    {
        cancellable.PerformCancellable(HideObject, ShowObject);
    }

    public virtual void SwitchTo3D(Cancellable cancellable)
    {
        cancellable.PerformCancellable(ShowObject, HideObject);
    }

}
