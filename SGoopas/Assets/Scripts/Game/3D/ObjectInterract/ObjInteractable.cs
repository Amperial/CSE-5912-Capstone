using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractable : MonoBehaviour {

    public enum ObjectType { pushPull, lift};
    public ObjectType objType;

    private void Awake()
    {
        if (objType == ObjectType.pushPull)
        {
            // Push objects don't have rotation and are really heavy until you interact with them.
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            gameObject.GetComponent<Rigidbody>().mass = 10.0F;
        }
    }

    /*
     * Implementation of interaction restrictions that are more complicated than proximity 
     * (e.g. for push pull, the player needs to be facing one of the object's faces)
     */
    public bool IsPlayerAbleToInteract(GameObject player) {
        switch (objType)
        {
            case ObjectType.pushPull:
                // Push pull is limited by the angle and distance.
                // Don't apply if the player is too close or not dead-on facing the box.
                Vector2 objDir = new Vector2(gameObject.transform.position.x - player.transform.parent.position.x, gameObject.transform.position.z - player.transform.parent.position.z);
                bool angleCorrect = Vector2.Angle(objDir.normalized, Vector2.up) < 15f || Vector2.Angle(objDir.normalized, Vector2.down) < 15f || Vector2.Angle(objDir.normalized, Vector2.left) < 15f || Vector2.Angle(objDir.normalized, Vector2.right) < 15f;
                float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
                return distance > 0.5 && angleCorrect;
            case ObjectType.lift:
            default:
                return true;
        }
    }
}
