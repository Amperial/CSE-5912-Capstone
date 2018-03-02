using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : State3DMove
    {
        private JumpCollider jumpScript;
        private float airVelocity, height, sides;
        private bool descending = false;
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            airVelocity = 3.0f;
            height = 0.499f;
            sides = 0.99f;
            jumpScript = player.GetComponent<JumpCollider>();
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

            if (rb.velocity.y < 0)
                descending = true;

            if (jumpScript.hit)
            {
                Vector3 playerPos = base.PlayerObject.transform.position;
                float playerbase = playerPos.y - height;
                bool land = false;
                foreach (ContactPoint contact in jumpScript.col.contacts)
                {
                    Vector3 cp = contact.point;
                    float xRange = Mathf.Abs(cp.x - playerPos.x);
                    float zRange = Mathf.Abs(cp.z - playerPos.z);
                    if (cp.y < playerbase && xRange < sides && zRange < sides && descending)
                    {
                        land = true;
                        break;
                    }
                }
                if (land)
                {
                    if (rb.velocity.magnitude < 0.01) {
                        SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));
                    }   
                    else {
                        SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
                    }
                        
                }
            }   
        }
    }

}
