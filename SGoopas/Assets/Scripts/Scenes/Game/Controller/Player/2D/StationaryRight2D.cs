﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class StationaryRight2D : Base2DState
    {
        public StationaryRight2D(BasePlayerState previousState) : base(previousState) {
            FlipSprite();
        }
        public StationaryRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) {
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
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(WalkForce, 0) * rb.mass, ForceMode2D.Force);
            SetState(new MovingRight2D(this));
        }

        public override void Update()
        {
            if (!Grounded.IsGrounded(PlayerObject.transform.position, characterWidth, GroundCheck.position))
            {
                SetState(new JumpingRight2D(this));
                Animator2D.updateGroundedParam(anim, false);
            }
            //Sets animator's x and y speeds for the animations to use
            Animator2D.updateXYParam(anim, rb);
        }
    }
}