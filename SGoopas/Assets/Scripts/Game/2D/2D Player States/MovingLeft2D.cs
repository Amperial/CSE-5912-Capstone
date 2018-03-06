using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovingLeft2D : Base2DState
    {
        public MovingLeft2D(BasePlayerState previousState) : base(previousState) { }
        public MovingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck) { }

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
            if (rb.velocity.x > -MaxHoriSpeed)
                rb.AddForce(new Vector2(-AirMoveForce, 0) * rb.mass, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            SetState(new StationaryLeft2D(this));
        }

        public override void Update()
        {
            if (!Physics2D.Linecast(PlayerObject.transform.position, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new JumpingLeft2D(this));
                anim.SetBool("grounded", false);
            }
            //Sets animator's x and y speeds for the animations to use
            anim.SetFloat("speedX", System.Math.Abs(rb.velocity.x));
            anim.SetFloat("speedY", rb.velocity.y);
        }
    }
}
