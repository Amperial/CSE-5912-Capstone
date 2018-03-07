using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingLeft2D : Base2DState
    {
        private bool dJump = true;
        private bool airDash = true;
        public JumpingLeft2D(BasePlayerState previousState) : base(previousState) {}
        public JumpingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {}
        
        public override void Action()
        {
            if (airDash)
            {
                airDash = false;
                if (rb.velocity.x > -MaxHoriSpeed)
                    rb.AddForce(new Vector2(-AirDashForce, 0) * rb.mass, ForceMode2D.Force);
            }
        }

        public override void FixedUpdate()
        {

        }

        public override void Jump()
        {
            if (dJump)
            {
                dJump = false;
                if(rb.velocity.y <= MaxVertSpeed)
                    rb.AddForce(new Vector2(0, JumpForce) * rb.mass, ForceMode2D.Force);
            }
        }

        public override void MoveDown()
        {
            
        }

        public override void MoveLeft()
        {
            if(rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-AirMoveForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(AirMoveForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void Update()
        {
            if (Grounded.IsGrounded(PlayerObject.transform.position, characterWidth, GroundCheck.position))
            {
                SetState(new StationaryLeft2D(this));
                Animator2D.updateGroundedParam(anim, true);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
