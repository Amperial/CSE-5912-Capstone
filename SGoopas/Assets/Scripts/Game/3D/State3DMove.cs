using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DMove : State3DStand
    {
        private float velocityCap;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        private Grabbing grabScript;
        float moveForceMagnitude = 50f;
        int stillFrames = 0;
        public State3DMove(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
           
            forwardForce = Vector3.forward;
            backForce = Vector3.back;
            rightForce = Vector3.right;
            leftForce = Vector3.left;
            velocityCap = 8.0f;
        }

        public override void MoveDown()
        {
            rb.AddForce(backForce * moveForceMagnitude);
        }

        public override void MoveLeft()
        {
            rb.AddForce(leftForce * moveForceMagnitude);
        }

        public override void MoveRight()
        {
            rb.AddForce(rightForce * moveForceMagnitude);
        }

        public override void MoveUp()
        {
            rb.AddForce(forwardForce * moveForceMagnitude);
        }

        public override void Release()
        {
            //no action
        }

        public override void Update()
        {
            //no-op for now.
        }

        public override void FixedUpdate()
        {
            ClipVelocity();
            CheckForStanding();
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
            if (nonVerticalVelocity.magnitude > 0.001)
            {
                Vector3 velocityDir = rb.velocity.normalized;
                yAngle = -Mathf.Atan2(-velocityDir.x, velocityDir.z) * Mathf.Rad2Deg;
            }
        }

        protected void ClipVelocity() {
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);

            if (nonVerticalVelocity.magnitude > velocityCap)
            {
                Vector2 cancelingForce = nonVerticalVelocity.normalized * -moveForceMagnitude;
                rb.AddForce(new Vector3(cancelingForce.x, 0, cancelingForce.y));
            }
        }

        private void CheckForStanding() {
            if (rb.velocity.magnitude < 0.001) {
                stillFrames++;
                if (stillFrames > 3) {
                    SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));   
                }
            } else {
                stillFrames = 0;
            }
        }

        public float yAngle
        {
            get { return base.PlayerObject.transform.rotation.eulerAngles.y; }
            set
            {
                Vector3 v = base.PlayerObject.transform.rotation.eulerAngles;
                base.PlayerObject.transform.rotation = Quaternion.Euler(v.x, value, v.z);
            }
        }
    }

}
