using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowController : MonoBehaviour {

    protected MeshRenderer meshRenderer;
    protected ShadowCaster shadowCaster;

    public virtual void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        shadowCaster = gameObject.GetComponent<ShadowCaster>();
    }

    public virtual void SwitchTo2D()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

    public virtual void SwitchTo3D()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

}
