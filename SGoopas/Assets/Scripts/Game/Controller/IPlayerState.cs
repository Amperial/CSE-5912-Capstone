using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public interface IPlayerState
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        void Jump();
        void Action();
        void Release();
    }
}
