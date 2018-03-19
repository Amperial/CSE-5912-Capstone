﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : State3DMove
    {

        public State3DJump(BasePlayerState previousState) : base(previousState) { moveForceMagnitude = 5f; }
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { moveForceMagnitude = 5f; }

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