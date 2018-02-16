﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowController : MonoBehaviour {

    protected MeshRenderer meshRenderer;
    protected ShadowCaster shadowCaster;
    protected LightCalculator lightCalculator;
    public Light light;
    public GameObject plane;
    public virtual void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        shadowCaster = gameObject.GetComponent<ShadowCaster>();

        if(light.type == LightType.Point)
            lightCalculator = new PointLightCalculator(light, plane);
        else
            lightCalculator = new DirectionalLightCalculator(light, plane);

    }

    public virtual void SwitchTo2D()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }

    public virtual void SwitchTo3D()
    {
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    public LightCalculator LightCalculator
    {
        set { lightCalculator = value; }
        get { return lightCalculator;}
    }
}
