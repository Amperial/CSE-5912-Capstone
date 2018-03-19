using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {
    List<Collider> availableObjects = new List<Collider>();

    public delegate void GrabAvailabilityChanged(List<Collider> availableObjects);
    public static event GrabAvailabilityChanged grabEvent;
    Shader highlight;
    public void Awake()
    {
        highlight = Shader.Find("Outlined/Silhouetted Diffuse"); ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ObjInteractable>() != null)
        {
            other.gameObject.GetComponent<Renderer>().material.shader = highlight;
            availableObjects.Add(other);
            grabEvent(availableObjects);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (availableObjects.Count > 0 && other.gameObject.GetComponent<ObjInteractable>() != null)
        {
            ObjInteractable script = other.gameObject.GetComponent<ObjInteractable>();
            other.gameObject.GetComponent<Renderer>().material.shader = script.normal;
            availableObjects.Remove(other);
            grabEvent(availableObjects);
        }
    }
}
