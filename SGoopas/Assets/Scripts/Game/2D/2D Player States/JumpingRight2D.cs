using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingRight2D : Base2DState
    {
        public JumpingRight2D(BasePlayerState previousState) : base(previousState) {
            FlipSprite();
        }
        public JumpingRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            FlipSprite();
        }


        private void FlipSprite()
        {
            Vector3 prevScale = PlayerObject.transform.localScale;
            prevScale.x = Mathf.Abs(prevScale.x);
            PlayerObject.transform.localScale = prevScale;
        }

        public override void Action()
        {
            if (DashTime != 0 && dash)
            {
                dash = false;
                Vector2 dashVec = DashVector;
                Vector3 dashVect = new Vector3(dashVec.x, dashVec.y, 0);
                SetState(new AirDashRight2D(this, dashVect));
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
            JumpingLeft2D left = new JumpingLeft2D(this);
            SetState(left);
            left.MoveLeft();
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
                SetState(new MovingRight2D(this));
                Animator2D.updateGroundedParam(anim, true);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
