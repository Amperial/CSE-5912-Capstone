using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DJump : Base3DState
    {
        private GameObject player3D;
        private MasterPlayerStateMachine master;
        private Rigidbody rb;
        private float jumpThreshold;
        private float airVelocity;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        public State3DJump(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            player3D = player;
            master = playerStateMachine;
            rb = player3D.GetComponent<Rigidbody>();
            jumpThreshold = 0.1f;
            forwardForce = new Vector3(0f, 0f, 5f);
            backForce = new Vector3(0f, 0f, -5f);
            rightForce = new Vector3(5f, 0f, 0f);
            leftForce = new Vector3(-5f, 0f, 0f);
            airVelocity = 1.0f;
        }
        public override void Action()
        {
            //cant interact while jumping
        }

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            //could not double jump
        }

        public override void MoveDown()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(backForce);
        }

        public override void MoveLeft()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(leftForce);
        }

        public override void MoveRight()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(rightForce);
        }

        public override void MoveUp()
        {
            if (CalcXZVel(rb) < airVelocity)
                rb.AddForce(forwardForce);
        }

        public override void Release()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            if (rb.velocity.y < jumpThreshold)
            {
                if (rb.velocity.magnitude == 0)
                    master.SetCurrentState(new State3DStand(player3D, master));
                else
                    master.SetCurrentState(new State3DMove(player3D, master));
            }   
        }

        private float CalcXZVel(Rigidbody rb)
        {
            float xSq = rb.velocity.x * rb.velocity.x;
            float ySq = rb.velocity.y * rb.velocity.y;
            return Mathf.Sqrt(xSq + ySq);
        }
    }

}
