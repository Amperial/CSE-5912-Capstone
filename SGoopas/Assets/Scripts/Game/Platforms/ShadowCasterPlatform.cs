using UnityEngine;

public abstract class ShadowCasterPlatform : MonoBehaviour
{
    /*
     * Does not inherit from original class
     */
    public Light shadowLight;
    public GameObject shadowPlane;
    protected GameObject shadow;

    //Added to class, possibly look at an inheritance instead of new file?
    public abstract void UpdateShadow(GameObject shadowObject);

    public abstract void CreateShadow();

    public GameObject GetShadow()
    {
        return shadow;
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
