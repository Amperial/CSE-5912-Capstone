﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class State3DStand : Base3DState
    {
        private List<Collider> grabbableObjects = new List<Collider>();

        public State3DStand(BasePlayerState previousState) : base(previousState) {
            if (previousState is State3DStand)
            {
                grabbableObjects = ((State3DStand)previousState).grabbableObjects;
            }
        }

        public State3DStand(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine) {}

        protected override void GrabAvailabilityChanged(List<Collider> availableObjects) 
        {
            grabbableObjects = availableObjects;
        }

        public override void Action()
        {
            if (grabbableObjects.Count > 0)
            {
                switch (grabbableObjects[0].gameObject.GetComponent<ObjInteractable>().objType)
                {
                    case ObjInteractable.ObjectType.pushPull:
                        SetState(new State3DGrab(grabbableObjects[0], this));
                        break;
                    case ObjInteractable.ObjectType.lift:
                        SetState(new State3DLift(grabbableObjects[0], this));
                        break;
                }
            }
        }

        public override void FixedUpdate()
        {
            // No-op.
        }

        public override void Jump()
        {
            rb.AddForce(new Vector3(0f, 5, 0f), ForceMode.Impulse);
            animation3D.JumpInPlace();
            SetState(new State3DJump(this));
        }

        public override void MoveDown()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveDown();
        }

        public override void MoveLeft()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveLeft();
        }

        public override void MoveRight()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveRight();
        }

        public override void MoveUp()
        {
            IPlayerState newState = new State3DMove(this);
            SetState(newState);
            newState.MoveUp();
        }

        public override void Release()
        {
            //no action
        }

        public override void Update()
        {
            // No-op.
        }
    }

}