using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovingLeft2D : Base2DState
    {
        public MovingLeft2D(BasePlayerState previousState) : base(previousState) {
            actionTaken = true;
            dash = true;
            dJump = true;
        }
        public MovingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            actionTaken = true;
        }

        bool actionTaken;

        public override void Action()
        {

        }

        public override void FixedUpdate()
        {
            if (rb.velocity.x >= 0)
                SetState(new StationaryLeft2D(this));
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
            actionTaken = true;
            if (rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-WalkForce, 0) * rb.mass * Time.deltaTime, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            SetState(new StationaryLeft2D(this));
        }

        public override void Update()
        {
            if (!IsGrounded)
            {
                SetState(new JumpingLeft2D(this));
                Animator2D.updateGroundedParam(anim, false);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }

        public override void LateUpdate()
        {
            if (!actionTaken)
            {
                SetState(new StationaryLeft2D(this));
            }
            actionTaken = false;
        }
    }
}
