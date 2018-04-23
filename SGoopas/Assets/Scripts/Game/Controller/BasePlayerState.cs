using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class BasePlayerState : IPlayerState
    {
        private GameObject player;
        private MasterPlayerStateMachine playerStateMachine;

        protected BasePlayerState(BasePlayerState previousState)
        {
            player = previousState.player;
            playerStateMachine = previousState.playerStateMachine;
        }

        protected BasePlayerState(GameObject player, MasterPlayerStateMachine playerStateMachine) {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
        }

        protected GameObject PlayerObject {
            get
            {
                return player;
            }
        }
        protected void SetState(IPlayerState newState)
        {
            playerStateMachine.SetCurrentState(newState);
        }

        public abstract void Action();
        public abstract void Jump();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void MoveUp();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void StoreState();
        public abstract void RestoreState();
        public abstract void LateUpdate();
        public abstract void Death();
        public abstract void EnemyCollision(GameObject Enemy);
        public abstract void ExitLevel();

        public virtual void Freeze() {
            SetState(new FrozenPlayerState(this));
        }

        public virtual void Unfreeze() {}
    }
}
