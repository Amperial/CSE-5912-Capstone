using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : State3DMove
    {
        private float moveForceMagnitude = 5f;
        public State3DJump(BasePlayerState previousState) : base(previousState) { }
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

        public override void Action()
        {
            //cant interact while jumping
        }

        public override void Jump()
        {
            //could not double jump
        }

        public override void MoveDown()
        {
            rb.AddForce(backForce * moveForceMagnitude);
        }

        public override void MoveLeft()
        {
            rb.AddForce(leftForce * moveForceMagnitude);
        }

        public override void MoveRight()
        {
            rb.AddForce(rightForce * moveForceMagnitude);
        }

        public override void MoveUp()
        {
            rb.AddForce(forwardForce * moveForceMagnitude);
        }

        public override void FixedUpdate()
        {
            
            base.FixedUpdate();
            if(rb.velocity.y < 0)
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
