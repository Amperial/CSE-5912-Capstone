using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private GameObject grabField;
        private MasterPlayerStateMachine master;
        private Rigidbody rb;
        private Grabbing grabScript;
        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
            master = playerStateMachine;
            Transform dummy = player.transform.Find("3DPlayer");
            grabField = dummy.Find("3DGrabField").gameObject;
            grabScript = grabField.GetComponent<Grabbing>();
        }
        public override void Action()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 300f, 0f));
            setState(new State3DJump(base.PlayerObject, master));
        }

        public override void MoveDown()
        {
            setState(new State3DMove(base.PlayerObject, master));
        }

        public override void MoveLeft()
        {
            master.SetCurrentState(new State3DMove(base.PlayerObject, master));
        }

        public override void MoveRight()
        {
            master.SetCurrentState(new State3DMove(base.PlayerObject, master));
        }

        public override void MoveUp()
        {
            master.SetCurrentState(new State3DMove(base.PlayerObject, master));
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }

}
