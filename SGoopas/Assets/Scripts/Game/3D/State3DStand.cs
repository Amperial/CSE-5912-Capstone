using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private GameObject grabField;
        private Rigidbody rb;
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
                grabScript.Grab();
                SetState(new State3DGrab(base.PlayerObject, base.MasterStateMachine));
            }
            else
            {
                //interact with other object
            }
        }

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 300f, 0f));
            SetState(new State3DJump(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveDown()
        {
            SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveLeft()
        {
            SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveRight()
        {
            SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
        }

        public override void MoveUp()
        {
            SetState(new State3DMove(base.PlayerObject, base.MasterStateMachine));
        }

        public override void Release()
        {
            //no action
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }

}
