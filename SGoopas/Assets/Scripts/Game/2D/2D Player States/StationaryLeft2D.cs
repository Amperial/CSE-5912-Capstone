using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class StationaryLeft2D : Base2DState
    {


        public StationaryLeft2D(BasePlayerState previousState) : base(previousState) {
            FlipSprite();
            rb.velocity = new Vector2(0, rb.velocity.y);
            dash = true;
            dJump = true;
        }
        public StationaryLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            FlipSprite();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        private void FlipSprite() {
            Vector3 prevScale = PlayerObject.transform.localScale;
            prevScale.x = -Mathf.Abs(prevScale.x);
            PlayerObject.transform.localScale = prevScale;
        }

        public override void Action()
        {
            if (dash)
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
            rb.AddForce(new Vector2(0, JumpForce) * rb.mass, ForceMode2D.Force);
            SetState(new JumpingLeft2D(this));
        }

        public override void MoveDown()
        {
            
        }

        public override void MoveLeft()
        {
            MovingLeft2D left = new MovingLeft2D(this);
            SetState(left);
            left.MoveLeft();
        }

        public override void MoveRight()
        {
            SetState(new StationaryRight2D(this));
            
        }

        public override void Update()
        {
            base.Update();
            if (!IsGrounded)
            {
                SetState(new JumpingLeft2D(this));
                Animator2D.updateGroundedParam(anim, false);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
