using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        private Vector2 objDir;
        public State3DGrab(Collider objectToGrab, BasePlayerState previousState) : base(previousState) {
            animation3D.StartGrab();
            animation3D.StopRun();
            startGrab(objectToGrab);
            moveForceMagnitude = 30f;
        }

        public State3DGrab(Collider objectToGrab, GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            animation3D.StartGrab();
            animation3D.StopRun();
            startGrab(objectToGrab);
            moveForceMagnitude = 30f;
        }

        private void startGrab(Collider objectToGrab)
        {
            objDir = new Vector2(objectToGrab.gameObject.transform.position.x - rb.gameObject.transform.position.x, objectToGrab.gameObject.transform.position.y - rb.gameObject.transform.position.y);
            grabJoint = PlayerObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = objectToGrab.attachedRigidbody;
        }

        public override void Action()
        {
            // No-op, disable action while grabbing.
        }

        public override void Jump()
        {
            // No-op, disable jumping while grabbing.
        }

        public override void Release()
        {
            animation3D.StopPush();
            animation3D.ReleaseGrab();
            Object.Destroy(grabJoint);
            SetState(new State3DStand(this));
        }

        public override void FixedUpdate() {
            // Disallow rotation while grabbing...
            ClipVelocity();
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
            if (nonVerticalVelocity.magnitude > 0.0001f)
            {
                animation3D.StartPush();
                if (Vector2.Dot(nonVerticalVelocity,objDir) < 0)
                {
                    animation3D.PullTrue();
                }
                else
                {
                    animation3D.PullFalse();
                }
            }
            else
                animation3D.StopPush();
        }
    }

}
