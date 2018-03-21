using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerStates
{
    public class DeadState2D : Base2DState
    {
        public DeadState2D(BasePlayerState previousState) : base(previousState) {
            ResetState();
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
            SetState(new StationaryRight2D(this));
        }
    }
}
