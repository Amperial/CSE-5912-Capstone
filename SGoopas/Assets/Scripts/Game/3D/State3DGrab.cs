using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        private Vector2 objDir;
        private bool upDown;
        public State3DGrab(Collider objectToGrab, BasePlayerState previousState) : base(previousState) {
            animation3D.StartGrab();
            animation3D.StopRun();
            upDown = false;
            startGrab(objectToGrab);
            moveForceMagnitude = 30f;
        }

        public State3DGrab(Collider objectToGrab, GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            animation3D.StartGrab();
            animation3D.StopRun();
            upDown = false;
            startGrab(objectToGrab);
            moveForceMagnitude = 30f;
        }

        private void startGrab(Collider objectToGrab)
        {
            objDir = new Vector2(objectToGrab.gameObject.transform.position.x - rb.gameObject.transform.position.x, objectToGrab.gameObject.transform.position.z - rb.gameObject.transform.position.z);
            if (Vector2.Angle(objDir.normalized, Vector2.up) < 45f || Vector2.Angle(objDir.normalized, Vector2.down) < 45f)
                upDown = true;
            
            rb.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(objDir.x,0,objDir.y));
            grabJoint = PlayerObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = objectToGrab.attachedRigidbody;
        }
        public override void MoveDown()
        {
            if(upDown)
                rb.AddForce(backForce * moveForceMagnitude);
        }

        public override void MoveLeft()
        {
            if (!upDown)
                rb.AddForce(leftForce * moveForceMagnitude);
        }

        public override void MoveRight()
        {
            if (!upDown)
                rb.AddForce(rightForce * moveForceMagnitude);
        }

        public override void MoveUp()
        {
            if (upDown)
                rb.AddForce(forwardForce * moveForceMagnitude);
        }
        public override void Action()
        {
            // No-op, disable action while grabbing.
        }

        public override void Jump()
        {
            // No-op, disable jumping while grabbing.
        }

        public override void Release()
        {
            animation3D.StopPush();
            animation3D.ReleaseGrab();
            Object.Destroy(grabJoint);
            SetState(new State3DStand(this));
        }

        public override void Update()
        {
            ApplyMovementForces();

            CheckForPushing();
        }

        private void CheckForPushing()
        {
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
            if (nonVerticalVelocity.magnitude > 0.0001f)
            {
                animation3D.StartPush();
                if (Vector2.Dot(nonVerticalVelocity,objDir) < 0)
                {
                    animation3D.PullTrue();
                }
                else
                {
                    animation3D.PullFalse();
                }
            }
            else
                animation3D.StopPush();
        }
        public override void FixedUpdate() {
            
        }
    }

}
