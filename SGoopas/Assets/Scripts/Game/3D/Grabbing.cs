using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {
    List<Collider> availableObjects = new List<Collider>();

    public delegate void GrabAvailabilityChanged(List<Collider> availableObjects);
    public static event GrabAvailabilityChanged grabEvent;
    Shader normal, highlight;
    public void Awake()
    {
        normal = Shader.Find("Diffuse");
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
        if (other.gameObject.GetComponent<ObjInteractable>() != null)
        {
            other.gameObject.GetComponent<Renderer>().material.shader = normal;
            availableObjects.Remove(other);
            grabEvent(availableObjects);
        }
    }
}
