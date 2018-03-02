using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class BasePlayerState : IPlayerState
    {
        private GameObject player;
        private MasterPlayerStateMachine playerStateMachine;

        protected BasePlayerState(GameObject player, MasterPlayerStateMachine playerStateMachine)
        {
        }

        public virtual void StartAsFirstState(GameObject player, MasterPlayerStateMachine playerStateMachine) {
            this.player = player;
            this.playerStateMachine = playerStateMachine;
        }
        
        protected MasterPlayerStateMachine MasterStateMachine {
            get
            {
                return playerStateMachine;
            }
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

        public virtual void TransitionFromState(IPlayerState previousState) {
            // All states at this point should derive from BasePlayerState.
            UnityEngine.Assertions.Assert.IsTrue(previousState is BasePlayerState);
            BasePlayerState previousStateBase = (BasePlayerState)previousState;
            player = previousStateBase.player;
            playerStateMachine = previousStateBase.playerStateMachine;
        }

        public abstract void GrabAvailabilityChanged(bool grabAvailable, Collider grabObject);
        public abstract void Action();
        public abstract void Jump();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void MoveUp();
        public abstract void Release();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void StoreState();
        public abstract void RestoreState();
    }
}
