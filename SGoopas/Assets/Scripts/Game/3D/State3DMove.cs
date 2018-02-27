using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DMove : State3DStand
    {
        private GameObject grabField;
        private Rigidbody rb;
        private float velocityCap;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        private Grabbing grabScript;
        float moveForceMagnitude = 50f;
        public State3DMove(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
           
            forwardForce = Vector3.forward;
            backForce = Vector3.back;
            rightForce = Vector3.right;
            leftForce = Vector3.left;
            velocityCap = 8.0f;
            grabField = player.transform.Find("3DGrabField").gameObject;
            grabScript = grabField.GetComponent<Grabbing>();
        }
        public override void Action()
        {
            if (grabScript.Grabbable)
            {
                grabScript.Grab();
                SetState(new State3DGrab(base.PlayerObject, base.MasterStateMachine));
            }
            else
            {
                //interact with other object
            }
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
            Vector2 nonVerticalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);

            if (nonVerticalVelocity.magnitude > velocityCap) {
                Vector2 cancelingForce = nonVerticalVelocity.normalized * -moveForceMagnitude;
                rb.AddForce(new Vector3(cancelingForce.x, 0, cancelingForce.y));
            }

            if (nonVerticalVelocity.magnitude < 0.001)
            {
                SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));
            }
            else {
                Vector3 velocityDir = rb.velocity.normalized;
                yAngle = -Mathf.Atan2(-velocityDir.x, velocityDir.z) * Mathf.Rad2Deg;
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
