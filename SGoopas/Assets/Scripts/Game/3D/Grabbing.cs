using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Grabbing : MonoBehaviour {
    List<Collider> availableObjects = new List<Collider>();

    public delegate void GrabAvailabilityChanged(List<Collider> availableObjects);
    public static event GrabAvailabilityChanged grabEvent;
    Material highlight;
    Material ogMaterial;
    GameObject highlightedObject;
    static string highlightName = "HighlightMaterial"; 

    public void Awake()
    {
        Shader highlightShader = Shader.Find("Outline/Transparent");
        Assert.IsNotNull(highlightShader, "Could not load highlight shader");
        highlight = new Material(highlightShader);
        highlight.name = highlightName;
    }
    private void OnTriggerEnter(Collider other)
    {
        ObjInteractable objInteractable = other.gameObject.GetComponent<ObjInteractable>();
        InteractTrigger trigger = other.gameObject.GetComponent<InteractTrigger>();
        bool objInteractableAvailable = objInteractable != null && objInteractable.IsPlayerAbleToInteract(gameObject);
        if (objInteractableAvailable || trigger != null)
        {
            HighlightObject(other.gameObject);
            availableObjects.Add(other);
            grabEvent(availableObjects);
        }
    }

    void HighlightObject(GameObject highlightObj) 
    {
        if (highlightedObject != null)
        {
            UnhighlightObject(highlightObj);
            availableObjects.Remove(highlightObj.GetComponent<Collider>());
        }
        ogMaterial = highlightObj.GetComponent<Renderer>().material;
        Material[] highligtMaterialSet = {ogMaterial, highlight};
        highlightObj.GetComponent<Renderer>().materials = highligtMaterialSet;
        highlightedObject = highlightObj;
    }

    void UnhighlightObject(GameObject unhighlightObj) 
    {
        highlightedObject = null;
        Material[] defaultMaterialSet = ogMaterial != null ? new Material[] { ogMaterial } : new Material[] {};
        unhighlightObj.GetComponent<Renderer>().materials = defaultMaterialSet;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(highlightedObject)) {
            UnhighlightObject(other.gameObject);
            availableObjects.Remove(other);
            grabEvent(availableObjects);
        }
    }
}
