using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base2DState : BasePlayerState
    {
        private Rigidbody2D rb;
        private Vector2 linearVelocity;
        private float angularVelocity;

        protected Base2DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
            rb = player.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            linearVelocity = new Vector2();
            angularVelocity = 0.0f;
        }

        public override void Release()
        {

        }

        public override void StoreState()
        {
            rb.isKinematic = false;

            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }

        public override void RestoreState()
        {
            rb.isKinematic = false;

            rb.velocity = linearVelocity;
            rb.angularVelocity = angularVelocity;
        }

    }
}
