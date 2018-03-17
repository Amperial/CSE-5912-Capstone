using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DLift : State3DMove
    {
        private GameObject grabObj;
        private Rigidbody grabRb;
        public State3DLift(Collider objectToGrab, BasePlayerState previousState) : base(previousState)
        {
            startGrab(objectToGrab);
    }

        public State3DLift(Collider objectToGrab, GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            startGrab(objectToGrab);
        }

        private void startGrab(Collider objectToGrab)
        {
            grabObj = objectToGrab.gameObject;
            grabRb = grabObj.GetComponent<Rigidbody>();
            grabRb.isKinematic = true;
            grabRb.detectCollisions = false;
            repositionObj();
            grabObj.transform.parent = rb.gameObject.transform;
        }
        private void repositionObj()
        {
            grabObj.transform.localRotation = rb.gameObject.transform.rotation;
            grabObj.transform.position = rb.gameObject.transform.position;
            float height =  grabObj.GetComponent<Renderer>().bounds.size.y;
            float width = grabObj.GetComponent<Renderer>().bounds.size.x;
            height = width / 2f + 1.2f;
            width = width / 2f -0.6f;
            Vector3 range = new Vector3(0f, height, width);
            grabObj.transform.Translate(range,Space.Self);
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
            animation3D.StopRun();
            animation3D.StopCarry();
            grabRb.isKinematic = false;
            grabRb.detectCollisions = true;
            grabObj.transform.parent = null;
            SetState(new State3DStand(this));
        }

        public override void FixedUpdate()
        {
            // Disallow rotation while grabbing...
            ClipVelocity();
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
            if (nonVerticalVelocity.magnitude > 0.0001f)
                animation3D.StartRun();
            else
                animation3D.StopRun();
            if (nonVerticalVelocity.magnitude > 0.001)
            {
                Vector3 velocityDir = rb.velocity.normalized;
                yAngle = -Mathf.Atan2(-velocityDir.x, velocityDir.z) * Mathf.Rad2Deg;
            }
        }
    }

}
