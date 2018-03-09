using UnityEngine;

namespace PlayerStates
{
    public class MasterPlayerStateMachine
    {

        private IPlayerState currentState, state2D, state3D, stateDisable;
        public IPlayerState CurrentPlayerState
        {
            get
            {
                return currentState;
            }

        }

        public void SetCurrentState(IPlayerState newState, bool store = false)
        {
            if (store) {
                if (currentState is Base2DState) {
                    state2D = currentState;
                } else if (currentState is Base3DState) {
                    state3D = currentState;
                }
                currentState.StoreState();
                currentState = newState;
                currentState.RestoreState();
            } else {
                currentState = newState;
            }
        }

        public MasterPlayerStateMachine(GameObject player2D, GameObject player3D)
        {
            //You can't instantiate an abstract class, the initial state of the players will be instantiated here, but for now
            state3D = new State3DStand(player3D, this);
            Transform groundCheck = player2D.transform.Find("GroundCheck");
            state2D = new StationaryRight2D(player2D, this, groundCheck);
            state2D.StoreState();
            stateDisable = new DisabledPlayerState();
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

        public void SwitchDimension()
        {
            currentState.SwitchDimension();
        }

        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void OnSwitchDimension(Dimension dimension, Cancellable cancellable) {
            // Determine new state and old state
            IPlayerState newState = dimension == Dimension.TWO_D ? state2D : state3D;
            IPlayerState oldState = currentState;

            // Perform state switch
            cancellable.PerformCancellable(() => SetCurrentState(newState, true), () => SetCurrentState(oldState, true));
        }
    }
}
