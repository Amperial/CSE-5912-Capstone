using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DGrab : State3DMove
    {
        private GameObject grabField;
        private Grabbing grabScript;
        private Joint grabJoint;
        public State3DGrab(BasePlayerState previousState) : base(previousState) {
            startGrab();
        }

        public State3DGrab(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {
            startGrab();
        }

        private void startGrab()
        {
            grabField = PlayerObject.transform.Find("3DGrabField").gameObject;
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
            SetState(new State3DStand(this));
        }

        public override void FixedUpdate() {
            // Disallow rotation while grabbing...
            ClipVelocity();
        }
    }

}
