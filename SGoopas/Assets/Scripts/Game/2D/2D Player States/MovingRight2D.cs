using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovingRight2D : Base2DState
    {
        public MovingRight2D(BasePlayerState previousState) : base(previousState) {
            actionTaken = true;
            dash = true;
            dJump = true;
            Animator2D.updateGroundedParam(anim, true);
        }
        public MovingRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            actionTaken = true;
            Animator2D.updateGroundedParam(anim, true);
        }

        bool actionTaken;

        public override void Action()
        {
            if (DashTime != 0 && dash)
            {
                dash = false;
                SetState(new AirDashRight2D(this, new Vector3(1, 0, 0)));
            }
        }

        public override void FixedUpdate()
        {
            if(rb.velocity.x <= 0)
                SetState(new StationaryRight2D(this));
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
            SetState(new StationaryRight2D(this));
        }

        public override void MoveRight()
        {
            actionTaken = true;
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(WalkForce, 0) * rb.mass * Time.deltaTime, ForceMode2D.Force);
        }

        public override void Update()
        {
            base.Update();
            if (!IsGrounded)
            {
                SetState(new JumpingRight2D(this));
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }

        public override void LateUpdate()
        {
            if (!actionTaken)
            {
                SetState(new StationaryRight2D(this));
            }
            actionTaken = false;
        }
    }
}
