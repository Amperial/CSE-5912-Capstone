using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSwappingIntroduction : MonoBehaviour, ITriggerable
{
    public PlayerCamera playerCamera;
    private static string[] swapIntroText = new string[] { "Oh, I almost forgot! When you swap controls, I'll freeze in midair. When you switch back to me, I'll unfreeze.", "This lets you move objects without having to worry about me falling." };

    public void Trigger()
    {
        DimensionSwitchHandler.TriggerDimensionSwitch();
        PlayerFreezeHandler.TriggerPlayerFreeze();
        playerCamera.FocusOnObject(MainObjectContainer.Instance.Player2D, new Vector3(0, -2, 3));
        MasterMonoBehaviour.Instance.DisplayMessage(swapIntroText, () => {
            PlayerFreezeHandler.TriggerPlayerUnfreeze();
            playerCamera.RestoreFocus();
        });
    }
}
