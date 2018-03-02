using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            
        }

        public override void GrabAvailabilityChanged(bool grabAvailable, Collider grabObject) {
            this.grabAvailable = grabAvailable;
            this.grabObject = grabObject;
        }

        public override void Action()
        {
            if (grabAvailable)
            {
                SetState(new State3DGrab(PlayerObject, MasterStateMachine));
            }
        }

        public override void FixedUpdate()
        {
            // No-op.
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 10, 0f), ForceMode.Impulse);
            SetState(new State3DJump(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveDown()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveDown();
        }

        public override void MoveLeft()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveLeft();
        }

        public override void MoveRight()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveRight();
        }

        public override void MoveUp()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveUp();
        }

        public override void Release()
        {
            //no action
        }

        public override void Update()
        {
            // No-op.
        }
    }

}
