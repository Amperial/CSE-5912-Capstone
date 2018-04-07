﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingLeft2D : Base2DState
    {
        public JumpingLeft2D(BasePlayerState previousState) : base(previousState) {
            FlipSprite();
        }
        public JumpingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
            FlipSprite();
        }


        private void FlipSprite()
        {
            Vector3 prevScale = PlayerObject.transform.localScale;
            prevScale.x = -Mathf.Abs(prevScale.x);
            PlayerObject.transform.localScale = prevScale;
        }
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
                Animator2D.updateGroundedParam(anim, true);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}
