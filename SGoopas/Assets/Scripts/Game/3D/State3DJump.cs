using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : State3DMove
    {
        private JumpCollider jumpScript;
        private float airVelocity = 3.0f;
        private float height = 0.499f;
        private float sides = 0.99f;
        private bool descending = false;
        public State3DJump(BasePlayerState previousState) : base(previousState) 
        {
            jumpScript = PlayerObject.GetComponent<JumpCollider>();
        }
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            jumpScript = PlayerObject.GetComponent<JumpCollider>();
        }

        public override void Action()
        {
            //cant interact while jumping
        }

        public override void Jump()
        {
            //could not double jump
        }

        public override void FixedUpdate()
        {
            
            base.FixedUpdate();
            if(rb.velocity.y <= 0)
            {
                bool land = Physics.Raycast(base.PlayerObject.transform.position, Vector3.down, 0.1f);

                if (land)
                {
                    if (rb.velocity.magnitude < 0.01)
                    {
                        SetState(new State3DStand(this));
                    }
                    else
                    {
                        SetState(new State3DMove(this));
                    }

                }
            }

        }
    }

}
