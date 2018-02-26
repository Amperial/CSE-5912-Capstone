using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : Base3DState
    {
        private JumpCollider jumpScript;
        private Rigidbody rb;
        private float airVelocity, height, sides;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
            forwardForce = new Vector3(0f, 0f, 15f);
            backForce = new Vector3(0f, 0f, -15f);
            rightForce = new Vector3(15f, 0f, 0f);
            leftForce = new Vector3(-15f, 0f, 0f);
            airVelocity = 3.0f;
            height = 0.499f;
            sides = 0.99f;
            jumpScript = player.GetComponent<JumpCollider>();
        }
        public override void Action()
        {
            //cant interact while jumping
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Jump()
        {
            //could not double jump
        }

        public override void MoveDown()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(backForce);
        }

        public override void MoveLeft()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(leftForce);
        }

        public override void MoveRight()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(rightForce);
        }

        public override void MoveUp()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(forwardForce);
        }

        public override void Release()
        {
            
        }

        public override void Update()
        {
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
                    if (cp.y < playerbase && xRange < sides && zRange < sides)
                    {
                        land = true;
                        break;
                    }
                }
                if (land)
                {
                    if (rb.velocity.magnitude == 0)
                        SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));
                    else
                        SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
                }
            }   
        }

        private float CalcXZVel(Rigidbody rb)
        {
            float xSq = rb.velocity.x * rb.velocity.x;
            float ySq = rb.velocity.y * rb.velocity.y;
            return Mathf.Sqrt(xSq + ySq);
        }
    }

}
