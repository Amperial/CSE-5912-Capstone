using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingRight2D : Base2DState
    {
        private bool dJump = true;
        private bool airDash = true;
        private Vector3 tempVR, tempVL;
        public JumpingRight2D(BasePlayerState previousState) : base(previousState) { }
        public JumpingRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) { }

        public override void Action()
        {
            if (airDash)
            {
                airDash = false;
                if (rb.velocity.x < MaxHoriSpeed)
                    rb.AddForce(new Vector2(AirDashForce, 0) * rb.mass, ForceMode2D.Force);
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
                if (rb.velocity.y <= MaxVertSpeed)
                    rb.AddForce(new Vector2(0, JumpForce) * rb.mass, ForceMode2D.Force);
            }
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            if (rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-AirMoveForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(AirMoveForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void Update()
        {
            tempVL = PlayerObject.transform.position;
            tempVR = PlayerObject.transform.position;
            tempVL.x = PlayerObject.transform.position.x - (characterWidth / 2.0f);
            tempVR.x = PlayerObject.transform.position.x + (characterWidth / 2.0f);
            
            if (Physics2D.Linecast(tempVL, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))) || Physics2D.Linecast(tempVR, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))) || Physics2D.Linecast(PlayerObject.transform.position, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new StationaryRight2D(this));
                anim.SetBool("grounded", true);
            }
            //Sets animator's x and y speeds for the animations to use
            anim.SetFloat("speedX", System.Math.Abs(rb.velocity.x));
            anim.SetFloat("speedY", rb.velocity.y);
        }
    }
}
