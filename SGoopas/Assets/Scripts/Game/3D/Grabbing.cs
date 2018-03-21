using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {
    List<Collider> availableObjects = new List<Collider>();

    public delegate void GrabAvailabilityChanged(List<Collider> availableObjects);
    public static event GrabAvailabilityChanged grabEvent;
    Shader highlight;
    public void Awake()
    {
        highlight = Shader.Find("Outlined/Silhouetted Diffuse");
    }
    private void OnTriggerEnter(Collider other)
    {

        ObjInteractable script = other.gameObject.GetComponent<ObjInteractable>();
        InteractTrigger buttonObj = other.gameObject.GetComponent<InteractTrigger>();
        if (script != null)
        {
            switch (script.objType)
            {
                case ObjInteractable.ObjectType.pushPull:
                    Vector2 objDir = new Vector2(other.gameObject.transform.position.x - gameObject.transform.parent.position.x, other.gameObject.transform.position.z - gameObject.transform.parent.position.z);
                    if (Vector2.Angle(objDir.normalized, Vector2.up) < 15f || Vector2.Angle(objDir.normalized, Vector2.down) < 15f || Vector2.Angle(objDir.normalized, Vector2.left) < 15f || Vector2.Angle(objDir.normalized, Vector2.right) < 15f)
                    {
                        other.gameObject.GetComponent<Renderer>().material.shader = highlight;
                        availableObjects.Add(other);
                        grabEvent(availableObjects);
                    }
                    break;

                case ObjInteractable.ObjectType.lift:
                    other.gameObject.GetComponent<Renderer>().material.shader = highlight;
                    availableObjects.Add(other);
                    grabEvent(availableObjects);
                    break;

            }

            //other.gameObject.GetComponent<Renderer>().material.shader = highlight;
            //availableObjects.Add(other);
            //grabEvent(availableObjects);
        }else if(buttonObj != null)
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
            ObjInteractable script = other.gameObject.GetComponent<ObjInteractable>();
            other.gameObject.GetComponent<Renderer>().material.shader = script.original;
            if (availableObjects.Contains(other))
                availableObjects.Remove(other);
            grabEvent(availableObjects);
        }else if(other.gameObject.GetComponent<InteractTrigger>() != null)
        {
            InteractTrigger buttonObj = other.gameObject.GetComponent<InteractTrigger>();
            other.gameObject.GetComponent<Renderer>().material.shader = buttonObj.original;
            if (availableObjects.Contains(other))
                availableObjects.Remove(other);
            grabEvent(availableObjects);
        }
    }
}
