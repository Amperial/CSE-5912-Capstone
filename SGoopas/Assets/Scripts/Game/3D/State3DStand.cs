using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private ObjInteractableBase grabbableObject;

        public State3DStand(BasePlayerState previousState) : base(previousState) {
            if (previousState is State3DStand)
            {
                grabbableObject = ((State3DStand)previousState).grabbableObject;
            }
        }

        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {}

        protected override void GrabbableObjectChanged(ObjInteractableBase grabbableObject) 
        {
            this.grabbableObject = grabbableObject;
        }

        public override void Action()
        {
            if (grabbableObject != null)
            {
                IPlayerState interactState = grabbableObject.PlayerBeganInteraction(this);
                if (interactState != null)
                {
                    SetState(interactState);
                }
            }
        }

        public override void FixedUpdate()
        {
            bool land = IsGrounded;
            if (!land)
            {
                SetState(new State3DJump(this));
            }
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 5, 0f), ForceMode.Impulse);
            animation3D.JumpInPlace();
            SetState(new State3DJump(this));
        }

        public override void MoveDown()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveDown();
        }

        public override void MoveLeft()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveLeft();
        }

        public override void MoveRight()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveRight();
        }

        public override void MoveUp()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveUp();
        }

        public override void Update()
        {
            // No-op.
        }
    }

}
