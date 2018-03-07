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

        protected Rigidbody2D rb;
        private Vector2 linearVelocity;
        private float angularVelocity;
        private Transform groundCheck;
        private Movement2DConfig mc;
        //
        protected Animator anim;
        protected float characterWidth;

        protected Base2DState(BasePlayerState previousState) : base(previousState)
        {
            if (previousState is Base2DState)
            {
                Base2DState previousState2D = (Base2DState)previousState;
                mc = previousState2D.mc;
                rb = previousState2D.rb;
                //
                anim = previousState2D.anim;
                characterWidth = previousState2D.characterWidth;
                linearVelocity = previousState2D.linearVelocity;
                angularVelocity = previousState2D.angularVelocity;
                groundCheck = previousState2D.groundCheck;
            }
        }

        protected Base2DState(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine)
        {
            mc = PlayerObject.GetComponent<Movement2DConfig>();
            rb = PlayerObject.GetComponent<Rigidbody2D>();
            //
            anim = PlayerObject.GetComponent<Animator>();
            characterWidth = PlayerObject.GetComponent<SpriteRenderer>().bounds.size.x/1.5f;

            linearVelocity = new Vector2();
            angularVelocity = 0.0f;
            this.groundCheck = groundCheck;
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
            // Commented out because MoveUp() is handled in the controller as an axis and will continually call Jump() while held down
            // Jump();
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
