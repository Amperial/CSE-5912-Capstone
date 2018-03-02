using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base3DState : BasePlayerState
    {
        private Vector3 linearVelocity;
        private Vector3 angularVelocity;
        protected Rigidbody rb;
        protected bool grabAvailable;
        protected Collider grabObject;

        protected Base3DState(BasePlayerState previousState) : base(previousState)
        {
            if (previousState is Base3DState)
            {
                Base3DState previousState3D = (Base3DState)previousState;
                rb = previousState3D.rb;
                linearVelocity = previousState3D.linearVelocity;
                angularVelocity = previousState3D.angularVelocity;
                grabObject = previousState3D.grabObject;
                grabAvailable = previousState3D.grabAvailable;
            }
        }

        protected Base3DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = PlayerObject.GetComponent<Rigidbody>();
            linearVelocity = new Vector3();
            angularVelocity = new Vector3();
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
