using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIntroduction : MonoBehaviour, ITriggerable {
    public PlayerCamera playerCamera;
    void ITriggerable.Trigger()
    {
        playerCamera.FocusOnObject(MainObjectContainer.Instance.Player2D);
        MasterMonoBehaviour.Instance.DisplayMessage(new string[]{"Hello there!"});

        //playerCamera.SeekObject(MainObjectContainer.Instance.Player3D);
    }
}
