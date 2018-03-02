using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base3DState : BasePlayerState
    {
        protected Rigidbody rb;
        private Vector3 linearVelocity;
        private Vector3 angularVelocity;
        protected Base3DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        }

        public override void StartAsFirstState(GameObject player, MasterPlayerStateMachine playerStateMachine)
        {
            base.StartAsFirstState(player, playerStateMachine);
            rb = PlayerObject.GetComponent<Rigidbody>();
            linearVelocity = new Vector3();
            angularVelocity = new Vector3();
        }

        /*
         * Transfer over any information that should be preserved across 3D states.
         */
        public override void TransitionFromState(IPlayerState previousState)
        {
            base.TransitionFromState(previousState);
            if (previousState is Base3DState)
            {
                Base3DState previousState3D = (Base3DState)previousState;
                rb = previousState3D.rb;
                linearVelocity = previousState3D.linearVelocity;
                angularVelocity = previousState3D.angularVelocity;
            }
        }

        public override void StoreState()
        {
            linearVelocity = rb.velocity;
            rb.velocity = new Vector3();
            angularVelocity = rb.angularVelocity;
            rb.angularVelocity = new Vector3();

            rb.isKinematic = true;
        }

        public override void RestoreState()
        {
            rb.isKinematic = false;

            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }
    }
}
