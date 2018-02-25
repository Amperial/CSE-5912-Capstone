using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class StationaryRight2D : Base2DState
    {
        public StationaryRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck)
        {
            Vector3 prevScale = player.transform.localScale;
            prevScale.x = Mathf.Abs(prevScale.x);
            player.transform.localScale = prevScale;
        }

        public override void Action()
        {

        }

        public override void FixedUpdate()
        {

        }

        public override void Jump()
        {
            Rigidbody2D rb2d = PlayerObject.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(0, JumpForce) * rb2d.mass, ForceMode2D.Force);
            SetState(new JumpingRight2D(PlayerObject, MasterStateMachine, GroundCheck));
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            SetState(new StationaryLeft2D(PlayerObject, MasterStateMachine, GroundCheck));
        }

        public override void MoveRight()
        {
            SetState(new MovingRight2D(PlayerObject, MasterStateMachine, GroundCheck));
        }

        public override void Update()
        {
            if (Physics2D.Linecast(PlayerObject.transform.position, GroundCheck.position, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                SetState(new JumpingRight2D(PlayerObject, MasterStateMachine, GroundCheck));
            }
        }
    }
}
