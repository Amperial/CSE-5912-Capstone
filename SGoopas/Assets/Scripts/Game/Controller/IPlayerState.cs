using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public interface IPlayerState
    {
        void StoreState();
        void RestoreState();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void Update();
        void FixedUpdate();
        void Jump();
        void Action();
        void Release();
        void GrabAvailabilityChanged(bool grabAvailable, Collider grabObject);
    }
}
