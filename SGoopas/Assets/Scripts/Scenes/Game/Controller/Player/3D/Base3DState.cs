﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerStates
{
    public abstract class Base3DState : BasePlayerState
    {
        private Vector3 linearVelocity;
        private Vector3 angularVelocity;
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
                Grabbing.grabEvent -= previousState3D.GrabAvailabilityChanged;
                Grabbing.grabEvent += this.GrabAvailabilityChanged;
            }
        }

        protected Base3DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = PlayerObject.GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            animation3D = new Animator3D(PlayerObject);
            linearVelocity = new Vector3();
            angularVelocity = new Vector3();
        }

        protected abstract void GrabAvailabilityChanged(List<Collider> availableObjects);

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