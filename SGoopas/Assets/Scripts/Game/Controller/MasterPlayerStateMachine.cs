using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MasterPlayerStateMachine
    {

        private IPlayerState currentState, state3D, state2D;
        public IPlayerState CurrentPlayerState
        {
            get
            {
                return currentState;
            }

        }

        public void SetCurrentState(IPlayerState newState)
        {
            currentState = newState;
        }

		public MasterPlayerStateMachine(GameObject player2D, GameObject player3D)
        {
            //You can't instantiate an abstract class, the initial state of the players will be instantiated here, but for now
            state3D = new State3DStand(player3D, this);
            Transform groundCheck = player2D.transform.Find("GroundCheck");
            state2D = new StationaryRight2D(player2D, this, groundCheck);
            state2D.StoreState();
            currentState = state3D;
            PlayerDeathHandler.ResetDeathEvent();
            PlayerDeathHandler.PlayerDeathEvent += PlayerDeathOccurred;
            ExitHandler.ResetExitEvent();
            ExitHandler.ExitEvent += PlayerExitsLevel;
            EnemyCollisionHandler.EnemyCollisionEvent += EnemyCollision;
        }

        ~MasterPlayerStateMachine()
        {
            // Unsubscribe from death and exit events when this object is destroyed.
            PlayerDeathHandler.PlayerDeathEvent -= PlayerDeathOccurred;
            ExitHandler.ExitEvent -= PlayerExitsLevel;
            EnemyCollisionHandler.EnemyCollisionEvent -= EnemyCollision;
        }

        public void PlayerDeathOccurred() {

            currentState.Death();
        }

        public void PlayerExitsLevel()
        {
            currentState.ExitLevel();
        }

        public void Action()
        {
            currentState.Action();
        }

        public void Jump()
        {
            currentState.Jump();
        }

        public void MoveDown()
        {
            currentState.MoveDown();
        }

        public void MoveLeft()
        {
            currentState.MoveLeft();
        }

        public void MoveRight()
        {
            currentState.MoveRight();
        }

        public void MoveUp()
        {
            currentState.MoveUp();
        }

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void LateUpdate()
        {
            currentState.LateUpdate();
        }

        public void SwitchDimension()
        {
            currentState.StoreState();

            if (currentState is Base2DState)
            {
                state2D = currentState;
                currentState = state3D;
            }
            else
            {
                state3D = currentState;
                currentState = state2D;
            }

            currentState.RestoreState();
        }

        /*
         * Supports cancelling a switch to 2D and a switch to 3D.
         */
        public void CancelDimensionSwitch()
        {
            currentState.StoreState();

            if (currentState is Base2DState)
            {
                state2D = currentState;
                currentState = state3D;

                // For some reason we couldn't swap to 2D. Give a visual cue.
                MasterMonoBehaviour.Instance.DisplayMessage(new string[]{"Echo: You can't swap in this position. You'll crush me!"});
            }
            else
            {
                state3D = currentState;
                currentState = state2D;
            }

            currentState.RestoreState();
        }

        public void EnemyCollision(GameObject Enemy)
        {
            currentState.EnemyCollision(Enemy);
        }
    }
}
