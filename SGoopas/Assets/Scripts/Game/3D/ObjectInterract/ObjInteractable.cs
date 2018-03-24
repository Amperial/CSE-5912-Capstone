using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractable : MonoBehaviour {

    public enum ObjectType { pushPull, lift};
    public ObjectType objType;

    /*
     * Implementation of interaction restrictions that are more complicated than proximity 
     * (e.g. for push pull, the player needs to be facing one of the object's faces)
     */
    public bool IsPlayerAbleToInteract(GameObject player) {
        switch (objType)
        {
            case ObjectType.pushPull:
                Vector2 objDir = new Vector2(gameObject.transform.position.x - player.transform.parent.position.x, gameObject.transform.position.z - player.transform.parent.position.z);
                return Vector2.Angle(objDir.normalized, Vector2.up) < 15f || Vector2.Angle(objDir.normalized, Vector2.down) < 15f || Vector2.Angle(objDir.normalized, Vector2.left) < 15f || Vector2.Angle(objDir.normalized, Vector2.right) < 15f;
            case ObjectType.lift:
            default:
                return true;
        }
    }
}
