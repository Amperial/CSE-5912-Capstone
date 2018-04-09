using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractable : ObjInteractableBase {

    public enum ObjectType { pushPull, lift};
    public enum AttachmentType { joints, hierarchy};
    public ObjectType objType;
    public AttachmentType attachmentType;

    public override void Awake()
    {
        base.Awake();
        if (objType == ObjectType.pushPull)
        {
            // Push objects don't have rotation and are really heavy until you interact with them.
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            ChangeNetMass(10.0f);
        }
    }

    public override void InteractionEnded()
    {
        if (objType == ObjectType.pushPull)
        {
            ChangeNetMass(10.0f);
        }
    }

    public override PlayerStates.IPlayerState PlayerBeganInteraction(PlayerStates.BasePlayerState currentState) {
        switch (objType)
        {
            case ObjectType.pushPull:
                ChangeNetMass(0.4f);
                return new PlayerStates.State3DGrab(gameObject.GetComponent<Collider>(), currentState);
            case ObjectType.lift:
                return new PlayerStates.State3DLift(gameObject.GetComponent<Collider>(), currentState);
            default:
                return null;
        }
    }

    private void ChangeNetMass(float netMass) {
        switch (attachmentType)
        {
            case AttachmentType.hierarchy:
                gameObject.GetComponent<Rigidbody>().mass = netMass;
                break;
            case AttachmentType.joints:
            default:
                foreach (GameObject pullObject in associatedObjects)
                {
                    pullObject.GetComponent<Rigidbody>().mass = netMass / associatedObjects.Length;
                }
                break;
        }
    }

    public override bool IsPlayerAbleToInteract(GameObject player) {
        switch (objType)
        {
            case ObjectType.pushPull:
                // Push pull is limited by the angle.
                // Don't apply if the player is too close or not dead-on facing the box.
                Vector2 objDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * player.transform.parent.rotation.eulerAngles.y), Mathf.Sin(Mathf.Deg2Rad * player.transform.parent.rotation.eulerAngles.y));
                bool angleCorrect = Vector2.Angle(objDir.normalized, Vector2.up) < 15f || Vector2.Angle(objDir.normalized, Vector2.down) < 15f || Vector2.Angle(objDir.normalized, Vector2.left) < 15f || Vector2.Angle(objDir.normalized, Vector2.right) < 15f;
                return angleCorrect;
            case ObjectType.lift:
            default:
                return true;
        }
    }
}
