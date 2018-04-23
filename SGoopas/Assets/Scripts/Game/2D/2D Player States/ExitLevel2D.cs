using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerStates
{
    public class ExitLevel2D : Base2DState
    {
        public ExitLevel2D(BasePlayerState previousState) : base(previousState)
        {
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
            rb.isKinematic = true;
        }

        public override void Action()
        {
            // No-op.
        }

        public override void FixedUpdate()
        {
            // No-op.
        }

        public override void Jump()
        {
            // No-op.
        }

        public override void MoveDown()
        {
            // No-op.
        }

        public override void MoveLeft()
        {
            // No-op.
        }

        public override void MoveRight()
        {
            // No-op.
        }
        public override void Update()
        {
            // No-op.
        }
    }
}
