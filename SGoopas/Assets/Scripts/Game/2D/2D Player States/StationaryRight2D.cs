using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class StationaryRight2D : Base2DState
    {
        public StationaryRight2D(BasePlayerState previousState) : base(previousState) {
            FlipSprite();
            rb.velocity = new Vector2(0, rb.velocity.y);
            dash = true;
            dJump = true;
        }
        public StationaryRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            FlipSprite();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        private void FlipSprite()
        {
            Vector3 prevScale = PlayerObject.transform.localScale;
            prevScale.x = Mathf.Abs(prevScale.x);
            PlayerObject.transform.localScale = prevScale;
        }

        public override void Action()
        {
        }

        public override void FixedUpdate()
        {

        }

        public override void Jump()
        {
            rb.AddForce(new Vector2(0, JumpForce) * rb.mass, ForceMode2D.Force);
            SetState(new JumpingRight2D(this));
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            SetState(new StationaryLeft2D(this));
        }

        public override void MoveRight()
        {
            MovingRight2D right = new MovingRight2D(this);
            SetState(right);
            right.MoveRight();
        }

        public override void Update()
        {
            base.Update();
            if (!IsGrounded)
            {
                SetState(new JumpingRight2D(this));
                Animator2D.updateGroundedParam(anim, false);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
