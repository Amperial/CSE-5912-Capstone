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
        Collider grabObject;

        public State3DGrab(Collider objectToGrab, BasePlayerState previousState) : base(previousState) {
            animation3D.StartGrab();
            animation3D.StopRun();
            upDown = false;
            startGrab(objectToGrab);
            moveForceMagnitude = 1200f;
        }

        public State3DGrab(Collider objectToGrab, GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            animation3D.StartGrab();
            animation3D.StopRun();
            upDown = false;
            startGrab(objectToGrab);
            moveForceMagnitude = 1200f;
        }

        private void startGrab(Collider objectToGrab)
        {
            grabObject = objectToGrab;
            float rAngle = Mathf.Deg2Rad * (90 - rb.gameObject.transform.rotation.eulerAngles.y);
            objDir = new Vector2(Mathf.Cos(rAngle), Mathf.Sin(rAngle));
            if (Vector2.Angle(objDir.normalized, Vector2.up) < 45f || Vector2.Angle(objDir.normalized, Vector2.down) < 45f)
                upDown = true;

            rb.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(objDir.x,0,objDir.y));
            grabJoint = PlayerObject.AddComponent<FixedJoint>();
            grabJoint.connectedBody = grabObject.attachedRigidbody;
        }
        public override void MoveDown()
        {
            if (upDown)
                base.MoveDown();
        }

        public override void MoveLeft()
        {
            if (!upDown)
                base.MoveLeft();
        }

        public override void MoveRight()
        {
            if (!upDown)
                base.MoveRight();
        }

        public override void MoveUp()
        {
            if (upDown)
                base.MoveUp();
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
            grabObject.GetComponent<ObjInteractableBase>().InteractionEnded();
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
