using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private Joint grabJoint;
        private bool upDown;
        Collider grabObject;
        private static Vector2[] axisAngles = new[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        private int angleIdx;

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
            Vector3 angleDiff = grabObject.transform.position - rb.transform.position;
            Vector2 angle2D = new Vector2(angleDiff.x, angleDiff.z);
            float maxDir = -1;
            angleIdx = 0;
            for (int idx = 0; idx < axisAngles.Length; idx++)
            {
                float curDir = Vector2.Dot(angle2D.normalized, axisAngles[idx]);
                if (maxDir < curDir)
                {
                    maxDir = curDir;
                    angleIdx = idx;
                }
            }
            Vector2 movePt;
            // 1,2 means up or down --> 3,4 means left right
            if (angleIdx < 2)
            {
                movePt = axisAngles[angleIdx] * Mathf.Abs(angle2D.y);
                upDown = true;
            }
            else
            {
                movePt = axisAngles[angleIdx] * Mathf.Abs(angle2D.x);
                upDown = false;
            }  
            rb.gameObject.transform.position = new Vector3(grabObject.transform.position.x - movePt.x, rb.gameObject.transform.position.y, grabObject.transform.position.z - movePt.y);
            rb.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(axisAngles[angleIdx].x,0, axisAngles[angleIdx].y));
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
            grabObject.GetComponent<ObjInteractableBase>().InteractionEnded();
            animation3D.StopPush();
            animation3D.ReleaseGrab();
            Object.Destroy(grabJoint);
            SetState(new State3DStand(this));
            angleIdx = 0;
        }

        public override void Jump()
        {
            // No-op, disable jumping while grabbing.
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
                if (Vector2.Dot(axisAngles[angleIdx], nonVerticalVelocity) < 0)
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
