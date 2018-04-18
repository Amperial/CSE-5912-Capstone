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
        private const float boxCastHeight = 0.1f;

        protected Rigidbody2D rb;
        private Vector2 linearVelocity;
        private float angularVelocity;
        private Transform groundCheck;
        private Vector3 original2DPosition;
        private Movement2DConfig mc;
        private bool lookingForGroundedPosition = true;

        protected Animator anim;
        protected Color mat;
        protected float characterWidth;
        protected bool dJump;
        protected bool dash;

        protected Base2DState(BasePlayerState previousState) : base(previousState)
        {
            if (previousState is Base2DState)
            {
                Base2DState previousState2D = (Base2DState)previousState;
                mc = previousState2D.mc;
                rb = previousState2D.rb;

                anim = previousState2D.anim;
                mat = previousState2D.mat;
                characterWidth = previousState2D.characterWidth;
                linearVelocity = previousState2D.linearVelocity;
                angularVelocity = previousState2D.angularVelocity;
                groundCheck = previousState2D.groundCheck;
                dJump = previousState2D.dJump;
                dash = previousState2D.dash;
                original2DPosition = previousState2D.original2DPosition;
                lookingForGroundedPosition = previousState2D.lookingForGroundedPosition;
            }
        }

        protected Base2DState(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine)
        {
            mc = PlayerObject.GetComponent<Movement2DConfig>();
            rb = PlayerObject.GetComponent<Rigidbody2D>();

            anim = PlayerObject.GetComponent<Animator>();

            characterWidth = PlayerObject.GetComponent<Collider2D>().bounds.size.x/1.2f;

            mat = PlayerObject.GetComponent<SpriteRenderer>().color;
            mat.a = 0.7f;
            PlayerObject.GetComponent<SpriteRenderer>().color = mat;

            linearVelocity = new Vector2();
            angularVelocity = 0.0f;
            this.groundCheck = groundCheck;
            dash = true;
            dJump = true;
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
            lookingForGroundedPosition = true;
        }

        public override void Update()
        {
            if (IsGrounded && lookingForGroundedPosition) {
                // Respawn player 2D at the first ground position found after a switch.
                // This prevents the case where the player gets stuck in the spot of death due to the user switching to 3D right beforehand.
                original2DPosition = rb.gameObject.transform.position;
                lookingForGroundedPosition = false;
            }
        }

        public override void LateUpdate()
        {
            
        }

        public override void Death() {
            SetState(new DeadState2D(this));
        }

        protected void ResetState()
        {
            rb.velocity = new Vector2();
            rb.angularVelocity = 0;
            rb.gameObject.transform.position = original2DPosition;
        }

        protected bool IsGrounded
        {
            get {
                Vector3 playerPosition = PlayerObject.transform.position;
                Vector3 ground = GroundCheck.position;

                return Physics2D.BoxCast(playerPosition, new Vector2(characterWidth, boxCastHeight), 0f, ground - playerPosition, (ground - playerPosition).magnitude, ~(1 << LayerMask.NameToLayer("Player")));
            }
        }
        public void Freeze()
        {
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0;
        }
        public override void ExitLevel()
        {
            anim.SetBool("exit", true);
            SetState(new ExitLevel2D(this));
        }

    }
}
