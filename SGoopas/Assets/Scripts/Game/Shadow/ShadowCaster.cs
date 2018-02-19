using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowCaster {

    protected Light shadowLight;
	protected GameObject shadowPlane;
	protected GameObject shadowObject;
    protected GameObject shadow;

	public abstract void CreateShadow();

	public ShadowCaster(Light shadowLight, GameObject shadowPlane, GameObject shadowObject) {
		this.shadowLight = shadowLight;
		this.shadowPlane = shadowPlane;
		this.shadowObject = shadowObject;
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
			UnityEngine.Object.Destroy(shadow);
        }
    }

}
