using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class BaseDashingState : Base2DState
    {
        public BaseDashingState(BasePlayerState previousState) : base(previousState) { }
        public BaseDashingState(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) { }

        public override void Action()
        {
            if (dash)
            {
                dash = false;
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
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, JumpForce) * rb.mass, ForceMode2D.Force);
            }
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            if (rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-AirMoveForce, 0) * rb.mass * Time.deltaTime, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(AirMoveForce, 0) * rb.mass * Time.deltaTime, ForceMode2D.Force);
        }

        public override void Update()
        {
            base.Update();
            if (IsGrounded)
            {
                SetState(new MovingLeft2D(this));
                Animator2D.updateGroundedParam(anim, true);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
