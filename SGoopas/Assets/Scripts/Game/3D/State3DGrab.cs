using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        public State3DGrab(BasePlayerState previousState) : base(previousState) {
            startGrab();
        }

        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            startGrab();
        }

        private void startGrab()
        {
            grabJoint = PlayerObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = grabbableObjects[0].attachedRigidbody;
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
            Object.Destroy(grabJoint);
            SetState(new State3DStand(this));
        }

        public override void FixedUpdate() {
            // Disallow rotation while grabbing...
            ClipVelocity();
        }
    }

}
