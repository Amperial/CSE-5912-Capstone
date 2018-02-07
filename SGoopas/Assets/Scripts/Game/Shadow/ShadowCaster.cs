using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShadowCaster : MonoBehaviour {

    public Light shadowLight;
    public GameObject shadowPlane;
    protected GameObject shadow;

    public abstract void CreateShadow();

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
