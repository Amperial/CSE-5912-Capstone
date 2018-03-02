using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {
    List<Collider> availableObjects = new List<Collider>();

    public delegate void GrabAvailabilityChanged(List<Collider> availableObjects);
    public static event GrabAvailabilityChanged grabEvent;

    private void OnTriggerEnter(Collider other)
    {
        availableObjects.Add(other);
        grabEvent(availableObjects);
    }

    void OnTriggerExit(Collider other)
    {
        availableObjects.Remove(other);
        grabEvent(availableObjects);
    }
}
