using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace PlayerStates
{
    public class FrozenPlayerState : BasePlayerState
    {
        private GameObject player;
        private MasterPlayerStateMachine playerStateMachine;
        BasePlayerState previousState;

        public FrozenPlayerState(BasePlayerState previousState) : base(previousState) {
            this.previousState = previousState;
        }

        public override void Action() {}
        public override void Jump() {}
        public override void MoveDown() {}
        public override void MoveLeft() {}
        public override void MoveRight() {}
        public override void MoveUp() {}
        public override void Update() {}
        public override void FixedUpdate() {}
        public override void StoreState() {}
        public override void RestoreState() {}
        public override void LateUpdate() {}
        public override void Death() {}
        public override void EnemyCollision(GameObject Enemy) {}
        public override void ExitLevel() {}

        public override void Freeze()
        {
            // No-op, can't freeze twice.
        }

        public override void Unfreeze()
        {
            Assert.IsNotNull(previousState, "Freeze state was entered without specifying a return state.");
            SetState(previousState);
        }
    }
}
