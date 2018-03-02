using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {}

        public override void TransitionFromState(IPlayerState previousState) {
            base.TransitionFromState(previousState);
            grabJoint = PlayerObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = grabObject.attachedRigidbody;
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
            SetState(new State3DStand(PlayerObject, MasterStateMachine));
        }

        public override void FixedUpdate() {
            // Disallow rotation while grabbing...
            ClipVelocity();
        }
    }

}
