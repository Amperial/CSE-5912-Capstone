using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private GameObject player3D;
        private MasterPlayerStateMachine master;
        private Rigidbody rb;
        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            player3D = player;
            master = playerStateMachine;
            rb = player3D.GetComponent<Rigidbody>();
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
            master.SetCurrentState(new State3DJump(player3D,master));
        }

        public override void MoveDown()
        {
            master.SetCurrentState(new State3DMove(player3D, master));
        }

        public override void MoveLeft()
        {
            master.SetCurrentState(new State3DMove(player3D, master));
        }

        public override void MoveRight()
        {
            master.SetCurrentState(new State3DMove(player3D, master));
        }

        public override void MoveUp()
        {
            master.SetCurrentState(new State3DMove(player3D, master));
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
