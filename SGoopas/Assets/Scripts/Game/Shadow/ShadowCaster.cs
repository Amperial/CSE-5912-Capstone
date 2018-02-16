using UnityEngine;

public abstract class ShadowCaster : MonoBehaviour {
    protected GameObject shadow;

    public abstract void CreateShadow(LightCalculator lightCalculator);

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
