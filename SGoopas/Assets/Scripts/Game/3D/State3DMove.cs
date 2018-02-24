using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DMove : Base3DState
    {
        private GameObject player3D;
        private MasterPlayerStateMachine master;
        private Rigidbody rb;
        private float velocity;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        public State3DMove(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            player3D = player;
            master = playerStateMachine;
            rb = player3D.GetComponent<Rigidbody>();
            forwardForce = new Vector3(0f, 0f, 60f);
            backForce = new Vector3(0f, 0f, -45f);
            rightForce = new Vector3(50f, 0f, 0f);
            leftForce = new Vector3(-50f, 0f, 0f);
            velocity = 5f;
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
            master.SetCurrentState(new State3DJump(player3D, master));
        }

        public override void MoveDown()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(backForce);
        }

        public override void MoveLeft()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(leftForce);
        }

        public override void MoveRight()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(rightForce);
        }

        public override void MoveUp()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(forwardForce);
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            if (rb.velocity.magnitude == 0)
                master.SetCurrentState(new State3DStand(player3D, master));
        }
    }

}
