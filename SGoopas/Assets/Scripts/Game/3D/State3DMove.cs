using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DMove : Base3DState
    {
        private GameObject grabField;
        private Rigidbody rb;
        private float velocity;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        private Grabbing grabScript;
        public State3DMove(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody>();
            forwardForce = new Vector3(0f, 0f, 60f);
            backForce = new Vector3(0f, 0f, -45f);
            rightForce = new Vector3(50f, 0f, 0f);
            leftForce = new Vector3(-50f, 0f, 0f);
            velocity = 5f;
            Transform dummy = player.transform.Find("3DPlayer");
            grabField = dummy.Find("3DGrabField").gameObject;
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

        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 300f, 0f));
           SetState(new State3DJump(base.PlayerObject, base.MasterStateMachine));
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
            //no action
        }

        public override void Update()
        {
            if (rb.velocity.magnitude == 0)
                SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));
        }
    }

}
