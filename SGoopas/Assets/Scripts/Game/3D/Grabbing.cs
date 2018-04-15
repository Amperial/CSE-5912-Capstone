﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Grabbing : MonoBehaviour {

    public delegate void GrabbableObjectChanged(ObjInteractableBase grabbableObject);
    public static event GrabbableObjectChanged grabEvent;
    ObjInteractableBase highlightedObject;

    private void Update()
    {
        if (highlightedObject != null && !highlightedObject.IsPlayerAbleToInteract(gameObject)) {
            UnhighlightObject(highlightedObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjInteractableBase objInteractable = other.gameObject.GetComponent<ObjInteractableBase>();
        bool objInteractableAvailable = objInteractable != null && objInteractable.IsPlayerAbleToInteract(gameObject);
        if (objInteractableAvailable)
        {
            HighlightObject(objInteractable);
        }
    }

    void HighlightObject(ObjInteractableBase newlyHighlightedObject) 
    {
        if (highlightedObject != null)
        {
            highlightedObject.UnhighlightObject();
        }
        newlyHighlightedObject.HighlightObject();
        highlightedObject = newlyHighlightedObject;
        grabEvent(highlightedObject);
    }

    void UnhighlightObject(ObjInteractableBase unhighlightObj) 
    {
        highlightedObject = null;
        unhighlightObj.UnhighlightObject();
        grabEvent(null);
    }

    void OnTriggerExit(Collider other)
    {
        ObjInteractableBase interactable =  other.GetComponent<ObjInteractableBase>();
        if (interactable != null && interactable.Equals(highlightedObject)) {
            UnhighlightObject(interactable);
        }
    }
}
