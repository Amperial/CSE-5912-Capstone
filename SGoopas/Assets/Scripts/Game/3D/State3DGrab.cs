using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : Base3DState
    {
        private GameObject player3D;
        private MasterPlayerStateMachine master;
        private Rigidbody rb;
        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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
            throw new System.NotImplementedException();
        }

        public override void MoveDown()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveLeft()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveRight()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        // Use this for initialization
        void Start()
        {

        }
    }

}
