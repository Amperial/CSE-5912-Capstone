using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovingLeft2D : Base2DState
    {
        private Rigidbody2D rb2d;
        public MovingLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck)
        {
            rb2d = player.GetComponent<Rigidbody2D>();
        }

        public override void Action()
        {

        }

        public override void FixedUpdate()
        {
            if (rb2d.velocity.x >= 0)
                SetState(new StationaryLeft2D(PlayerObject, MasterStateMachine, GroundCheck));
        }

        public override void Jump()
        {
            rb2d.AddForce(new Vector2(0, JumpForce) * rb2d.mass, ForceMode2D.Force);
            SetState(new JumpingLeft2D(PlayerObject, MasterStateMachine, GroundCheck));
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
            SetState(new StationaryLeft2D(PlayerObject, MasterStateMachine, GroundCheck));
        }

        public override void Update()
        {
            if (!Physics2D.Linecast(PlayerObject.transform.position, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new JumpingLeft2D(PlayerObject, MasterStateMachine, GroundCheck));
            }
        }
    }
}
