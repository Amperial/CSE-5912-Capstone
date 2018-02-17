using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowCaster : MonoBehaviour {

    protected Light shadowLight;
    protected GameObject shadowPlane;
    protected GameObject shadow;

    public abstract void CreateShadow();

	public void ConfigureWithLightParams(Light shadowLight, GameObject shadowPlane) {
		this.shadowLight = shadowLight;
		this.shadowPlane = shadowPlane;
	}

    public void ShowShadow()
    {
        if (shadow != null)
        {
            shadow.SetActive(true);
        }
    }

    public void HideShadow()
    {
        if (shadow != null)
        {
            shadow.SetActive(false);
        }
    }

    public void DestroyShadow()
    {
        if (shadow != null)
        {
            Destroy(shadow);
        }
    }

}
