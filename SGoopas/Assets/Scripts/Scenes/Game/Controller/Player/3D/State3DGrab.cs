using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        public State3DGrab(Collider objectToGrab, BasePlayerState previousState) : base(previousState) {
            startGrab(objectToGrab);
        }

        public State3DGrab(Collider objectToGrab, GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            startGrab(objectToGrab);
        }

        private void startGrab(Collider objectToGrab)
        {
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

        public override void SwitchDimension()
        {
            // No-op, disable switching dimensions while grabbing.
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
                animation3D.StartPush();
            else
                animation3D.StopPush();
        }
    }

}
