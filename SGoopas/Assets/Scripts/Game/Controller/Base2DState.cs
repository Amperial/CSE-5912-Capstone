using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base2DState : BasePlayerState
    {

        protected Base2DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {

        }

        public override void Release()
        {

        }

    }
}
