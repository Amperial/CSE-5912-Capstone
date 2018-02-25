using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : Base3DState
    {
        private GameObject player3D, grabField;
        private Rigidbody rb;
        private Vector3 forwardForce, backForce, rightForce, leftForce;
        private float velocity;
        private Grabbing grabScript;
        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            player3D = player;
            rb = player3D.GetComponent<Rigidbody>();
            forwardForce = new Vector3(0f, 0f, 60f);
            backForce = new Vector3(0f, 0f, -45f);
            rightForce = new Vector3(50f, 0f, 0f);
            leftForce = new Vector3(-50f, 0f, 0f);
            velocity = 5f;
            grabField = player.transform.Find("3DGrabField").gameObject;
            grabScript = grabField.GetComponent<Grabbing>();
        }
        public override void Action()
        {
            //interract with other triggers (NOT GRABBING)
        }

        public override void FixedUpdate()
        {
            
        }

        public override void Jump()
        {
            //cant jump while grabbing
        }

        public override void MoveDown()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(backForce);
        }

        public override void MoveLeft()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(backForce);
        }

        public override void MoveRight()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(backForce);
        }

        public override void MoveUp()
        {
            if (rb.velocity.magnitude < velocity)
                rb.AddForce(backForce);
        }

        public override void Release()
        {
            grabScript.Release();
        }

        public override void Update()
        {
            
        }

        // Use this for initialization
        void Start()
        {

        }
    }

}
