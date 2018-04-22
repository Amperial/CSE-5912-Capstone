using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DMove : State3DStand
    {
        private Vector3 moveDir = new Vector3();
        private float velocityCap = 6.0f;
        protected Vector3 forwardForce = Vector3.forward;
        protected Vector3 backForce = Vector3.back;
        protected Vector3 rightForce = Vector3.right;
        protected Vector3 leftForce = Vector3.left;
        protected float moveForceMagnitude = 1200f;
        int stillFrames = 0;

        public State3DMove(BasePlayerState previousState) : base(previousState) {}
        public State3DMove(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {}
        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 5, 0f), ForceMode.Impulse);
            animation3D.Jump();
            SetState(new State3DJump(this));
        }
        public override void MoveDown()
        {
            moveDir += backForce;
        }

        public override void MoveLeft()
        {
            moveDir += leftForce;
        }

        public override void MoveRight()
        {
            moveDir += rightForce;
        }

        public override void MoveUp()
        {
            moveDir += forwardForce;
        }

        public override void Update()
        {
            ApplyMovementForces();

            ChangeRotation();

            CheckForStanding();
        }

        protected void ApplyMovementForces()
        {
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
            if (!(nonVerticalVelocity.magnitude > velocityCap))
            {
                rb.AddForce(moveDir.normalized * moveForceMagnitude * Time.deltaTime);
                moveDir.Set(0, 0, 0);
            }
            else
            {
                ClipVelocity(nonVerticalVelocity);
            }
        }

        protected void ChangeRotation()
        {
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);

            if (nonVerticalVelocity.magnitude > 0.001)
            {
                Vector3 velocityDir = rb.velocity.normalized;
                yAngle = -Mathf.Atan2(-velocityDir.x, velocityDir.z) * Mathf.Rad2Deg;
            }
        }

        private void ClipVelocity(Vector2 nonVerticalVelocity) {
            Vector2 newVelocity = nonVerticalVelocity.normalized * velocityCap;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.y);
        }

        private void CheckForStanding() {
            if (rb.velocity.magnitude < 0.001) {
                stillFrames++;
                if (stillFrames > 3) {
                    animation3D.StopRun();
                    SetState(new State3DStand(this));   
                }
            } else {
                stillFrames = 0;
                animation3D.StartRun();
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
