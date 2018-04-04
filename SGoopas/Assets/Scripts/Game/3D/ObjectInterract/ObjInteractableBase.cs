﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
 * Contains an implementation for highlighting objects and an interface for how the player uses the interactables.
 */
public abstract class ObjInteractableBase : MonoBehaviour
{
    // This allows you to configure multi-object interactables.
    public GameObject[] associatedObjects;

    private static string highlightName = "HighlightMaterial";
    private static Material highlightMaterial;
    private static List<Material> originalMaterials;

    static Material GetHighlightMaterial()
    {
        if (!highlightMaterial) {
            Shader highlightShader = Shader.Find("Outline/Transparent");
            Assert.IsNotNull(highlightShader, "Could not load highlight shader");
            highlightMaterial = new Material(highlightShader);
            highlightMaterial.name = highlightName;
        }
        return highlightMaterial;
    }

    public virtual void Awake()
    {
        // If the associatedObjects field isn't filled, assume the interactable only applies to the root object.
        if (associatedObjects == null || associatedObjects.Length == 0)
        {
            associatedObjects = new GameObject[] { gameObject };
        }
    }

    public virtual void HighlightObject() {
        originalMaterials = new List<Material>();
        foreach (GameObject highlightObj in associatedObjects)
        {
            Material ogMaterial = highlightObj.GetComponent<Renderer>().material;
            Material[] highlightMaterialSet = {ogMaterial, GetHighlightMaterial()};
            highlightObj.GetComponent<Renderer>().materials = highlightMaterialSet;
            originalMaterials.Add(ogMaterial);
        }
    }

    public virtual void UnhighlightObject() {
        if (originalMaterials.Count == associatedObjects.Length)
        {
            int i = 0;
            foreach (GameObject highlightObj in associatedObjects) 
            {
                Material ogMaterial = originalMaterials[i];
                Material[] defaultMaterialSet = ogMaterial != null ? new Material[] { ogMaterial } : new Material[] { };
                highlightObj.GetComponent<Renderer>().materials = defaultMaterialSet;
                i++;
            }
        }
    }

    /*
     * Called when the player chooses to initiate an interaction. If the interaction requires a change in the player state, this state should be returned in this method.
     */
    public abstract PlayerStates.IPlayerState PlayerBeganInteraction(PlayerStates.BasePlayerState currentState);

    /*
     * Called right when the interaction ends.
     */
    public abstract void InteractionEnded();

    /*
     * Implementation of interaction restrictions that are more complicated than proximity 
     * (e.g. for push pull, the player needs to be facing one of the object's faces)
     */
    public abstract bool IsPlayerAbleToInteract(GameObject player);
}