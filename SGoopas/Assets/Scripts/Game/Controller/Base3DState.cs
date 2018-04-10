using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerStates
{
    public abstract class Base3DState : BasePlayerState
    {
        private Vector3 linearVelocity;
        private Vector3 angularVelocity;
        private Collider guardCollider;
        protected Rigidbody rb;
        protected Animator3D animation3D;

        protected Base3DState(BasePlayerState previousState) : base(previousState)
        {
            if (previousState is Base3DState)
            {
                Base3DState previousState3D = (Base3DState)previousState;
                rb = previousState3D.rb;
                animation3D = previousState3D.animation3D;
                linearVelocity = previousState3D.linearVelocity;
                angularVelocity = previousState3D.angularVelocity;
                Grabbing.grabEvent -= previousState3D.GrabbableObjectChanged;
                Grabbing.grabEvent += this.GrabbableObjectChanged;
                guardCollider = previousState3D.guardCollider;
            }
        }

        protected Base3DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = PlayerObject.GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            animation3D = new Animator3D(PlayerObject);
            linearVelocity = new Vector3();
            angularVelocity = new Vector3();
            guardCollider = player.GetComponent<Collider>();
        }

        protected abstract void GrabbableObjectChanged(ObjInteractableBase grabbableObject);

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

        public override void LateUpdate()
        {

        }

        public override void Death()
        {
            // No-op, you can't die in 3D.
        }

        protected bool IsGrounded {
            get
            {
                return Physics.Raycast(new Vector3(guardCollider.bounds.center.x, guardCollider.bounds.min.y + 0.1f, guardCollider.bounds.center.z), Vector3.down, 0.2f) || 
                    Physics.Raycast(new Vector3(guardCollider.bounds.min.x, guardCollider.bounds.min.y + 0.1f, guardCollider.bounds.min.z), Vector3.down, 0.2f)  || 
                    Physics.Raycast(new Vector3(guardCollider.bounds.min.x, guardCollider.bounds.min.y + 0.1f, guardCollider.bounds.max.z), Vector3.down, 0.2f) || 
                    Physics.Raycast(new Vector3(guardCollider.bounds.max.x, guardCollider.bounds.min.y + 0.1f, guardCollider.bounds.min.z), Vector3.down, 0.2f)  || 
                    Physics.Raycast(new Vector3(guardCollider.bounds.max.x, guardCollider.bounds.min.y + 0.1f, guardCollider.bounds.max.z), Vector3.down, 0.2f);
            }
        }

    }
}
