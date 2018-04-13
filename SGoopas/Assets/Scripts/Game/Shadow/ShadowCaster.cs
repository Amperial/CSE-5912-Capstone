using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowCaster {

    protected Light shadowLight;
	protected GameObject shadowPlane;
	protected GameObject shadowObject;
    protected GameObject shadow;

    public abstract void UpdateShadow();

    public ShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) {
		this.shadowLight = shadowLight;
		this.shadowPlane = shadowPlane;
		this.shadowObject = shadowObject;
	}

    public GameObject GetShadow()
    {
        return shadow;
    }

    public virtual void CreateShadow()
    {
        if (shadow)
        {
            DestroyShadow();
        }
    }

    public void ShowShadow()
    {
        if (shadow == null)
        {
            // You can call ShowShadow without having to call CreateShadow.
            // This allows for lazy initialization.
            CreateShadow();
        } else {
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
			UnityEngine.Object.Destroy(shadow);
        }
    }
}
