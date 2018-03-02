using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private GameObject grabField;
        private Grabbing grabScript;
        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
            grabField = player.transform.Find("3DGrabField").gameObject;
            grabScript = grabField.GetComponent<Grabbing>();
        }
        public override void Action()
        {
            if (grabScript.Grabbable)
            {
                SetState(new State3DGrab(base.PlayerObject, base.MasterStateMachine));
                grabScript.Grab();
            }
            else
            {
                //interact with other object
            }
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 10, 0f), ForceMode.Impulse);
            SetState(new State3DJump(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveDown()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveDown();
        }

        public override void MoveLeft()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveLeft();
        }

        public override void MoveRight()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveRight();
        }

        public override void MoveUp()
        {
            IPlayerState newState = new State3DMove(base.PlayerObject, base.MasterStateMachine);
            SetState(newState);
            newState.MoveUp();
        }

        public override void Release()
        {
            //no action
        }

        public override void Update()
        {
        }
    }

}
