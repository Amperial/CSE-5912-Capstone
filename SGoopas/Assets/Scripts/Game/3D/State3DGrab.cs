using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private GameObject player3D, grabField;
        private Rigidbody rb;
        private Grabbing grabScript;
        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            player3D = player;
            rb = player3D.GetComponent<Rigidbody>();
            grabField = player.transform.Find("3DGrabField").gameObject;
            grabScript = grabField.GetComponent<Grabbing>();
        }
        public override void Action()
        {
            //interract with other triggers (NOT GRABBING)
            grabScript.PutDown();
        }

        public override void Jump()
        {
            grabScript.lift();
        }

        public override void Release()
        {
            grabScript.Release();
            SetState(new State3DStand(base.PlayerObject, base.MasterStateMachine));
        }

        public override void FixedUpdate() {
            // Disallow rotation while grabbing...
            ClipVelocity();
        }
    }

}
