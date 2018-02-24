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
            //state2D = new Base2DState(player2D, this);
            currentState = state3D;
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

        public void Release()
        {
            currentState.Release();
        }

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
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
    }
}
