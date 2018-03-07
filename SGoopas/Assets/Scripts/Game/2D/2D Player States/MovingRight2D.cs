using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovingRight2D : Base2DState
    {
        private Vector3 tempVR, tempVL;
        public MovingRight2D(BasePlayerState previousState) : base(previousState) { }
        public MovingRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) { }

        public override void Action()
        {
           
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
            if (rb.velocity.x < MaxHoriSpeed)
                rb.AddForce(new Vector2(WalkForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void Update()
        {
            tempVL = PlayerObject.transform.position;
            tempVR = PlayerObject.transform.position;
            tempVL.x = PlayerObject.transform.position.x - (characterWidth / 2.0f);
            tempVR.x = PlayerObject.transform.position.x + (characterWidth / 2.0f);
            if (!Physics2D.Linecast(tempVL, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))) || !Physics2D.Linecast(tempVR, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new JumpingRight2D(this));
                anim.SetBool("grounded", false);
            }
            //Sets animator's x and y speeds for the animations to use
            anim.SetFloat("speedX", System.Math.Abs(rb.velocity.x));
            anim.SetFloat("speedY", rb.velocity.y);
        }
    }
}
