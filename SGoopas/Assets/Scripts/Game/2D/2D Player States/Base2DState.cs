using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base2DState : BasePlayerState
    {
        private float airMoveForce = 1f;
        private float jumpForce = 1f;
        private float airDashForce = 1f;
        private float walkForce = 1f;
        private float dJumpForce = 1f;
        private float maxHoriSpeed = 1f;
        private float maxVertSpeed = 1f;

        private Rigidbody2D rb;
        private Vector2 linearVelocity;
        private float angularVelocity;
        private Transform groundCheck;
        private Movement2DConfig mc;
        protected Base2DState(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine)
        {
            this.groundCheck = groundCheck;
            rb = player.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            linearVelocity = new Vector2();
            angularVelocity = 0.0f;
            Movement2DConfig mc = PlayerObject.GetComponent<Movement2DConfig>();
        }


        protected Transform GroundCheck {
            get
            {
                return groundCheck;
            }
        }

        public float AirMoveForce
        {
            get
            {
                if (mc)
                    return mc.airMoveForce;
                return airMoveForce;
            }
        }

        public float JumpForce
        {
            get
            {
                if (mc)
                    return mc.jumpForce;
                return jumpForce;
            }
        }

        public float AirDashForce
        {
            get
            {
                if (mc)
                    return mc.airDashForce;
                return airDashForce;
            }
        }

        public float WalkForce
        {
            get
            {
                if (mc)
                    return mc.walkForce;
                return walkForce;
            }
        }

        public float DJumpForce
        {
            get
            {
                if (mc)
                    return mc.dJumpForce;
                return dJumpForce;
            }
        }

        public float MaxHoriSpeed
        {
            get
            {
                if (mc)
                    return mc.maxHoriSpeed;
                return maxHoriSpeed;
            }
        }

        public float MaxVertSpeed
        {
            get
            {
                if (mc)
                    return mc.maxVertSpeed;
                return maxVertSpeed;
            }
        }

        public override void Release()
        {

        }
        public override void MoveUp()
        {
            Jump();
        }

        public override void StoreState()
        {
            rb.isKinematic = true;

            linearVelocity = rb.velocity;
            rb.velocity = new Vector2();
            angularVelocity = rb.angularVelocity;
            rb.angularVelocity = 0.0f;
        }

        public override void RestoreState()
        {
            rb.isKinematic = false;

            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }

    }
}
