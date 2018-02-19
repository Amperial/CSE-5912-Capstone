using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public abstract class Base3DState : BasePlayerState
    {
        protected Base3DState(GameObject player, MasterPlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {

        }
        
    }
}
