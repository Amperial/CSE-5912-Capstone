using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class JumpingRight2D : Base2DState
    {
        private bool dJump = true;
        private bool airDash = true;
        private Rigidbody2D rb2d;
        public JumpingRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck)
        {
            rb2d = player.GetComponent<Rigidbody2D>();
        }

        public override void Action()
        {
            if (airDash)
            {
                airDash = false;
                if (rb2d.velocity.x < MaxHoriSpeed)
                    rb2d.AddForce(new Vector2(AirDashForce, 0) * rb2d.mass, ForceMode2D.Force);
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
                if (rb2d.velocity.y <= MaxVertSpeed)
                    rb2d.AddForce(new Vector2(0, JumpForce) * rb2d.mass, ForceMode2D.Force);
            }
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            if (rb2d.velocity.x > -MaxHoriSpeed)
                rb2d.AddForce(new Vector2(-AirMoveForce, 0) * rb2d.mass, ForceMode2D.Force);
        }

        public override void MoveRight()
        {
            if (rb2d.velocity.x < MaxHoriSpeed)
                rb2d.AddForce(new Vector2(AirMoveForce, 0) * rb2d.mass, ForceMode2D.Force);
        }

        public override void Update()
        {
            if (Physics2D.Linecast(PlayerObject.transform.position, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new StationaryRight2D(PlayerObject, MasterStateMachine, GroundCheck));
            }
        }
    }
}
