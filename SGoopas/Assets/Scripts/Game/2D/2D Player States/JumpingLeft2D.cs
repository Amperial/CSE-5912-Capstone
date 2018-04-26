using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingLeft2D : Base2DState
    {
        public JumpingLeft2D(BasePlayerState previousState) : base(previousState) {
            MakeSpriteFaceLeft();
            Animator2D.updateGroundedParam(anim, false);
        }
        public JumpingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            MakeSpriteFaceLeft();
            Animator2D.updateGroundedParam(anim, false);
        }


        
        public override void Action()
        {
            if (DashTime != 0 && dash)
            {
                dash = false;
                Vector2 dashVec = DashVector;
                Vector3 dashVect = new Vector3(dashVec.x, dashVec.y, 0);
                SetState(new AirDashLeft2D(this, dashVect));
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
            if(rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-AirMoveForce, 0) * rb.mass * Time.deltaTime, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            JumpingRight2D right = new JumpingRight2D(this);
            SetState(right);
            right.MoveRight();
        }

        public override void Update()
        {
            base.Update();
            if (IsGrounded)
            {
                SetState(new MovingLeft2D(this));
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
