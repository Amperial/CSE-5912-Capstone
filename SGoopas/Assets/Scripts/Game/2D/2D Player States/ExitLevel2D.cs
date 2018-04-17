using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerStates
{
    public class ExitLevel2D : Base2DState
    {
        public ExitLevel2D(BasePlayerState previousState) : base(previousState)
        {
            Freeze();
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
            SetState(new ExitLevel2D(this));
        }
    }
}
