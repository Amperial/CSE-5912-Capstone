using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIntroduction : MonoBehaviour, ITriggerable {
    public PlayerCamera playerCamera;
    private static string[] introductionText = new string[] {
        "Hello there! My name is Echo. I'm trying to get home, but I can't seem to make the jump across these blocks.",
        "Maybe you can help me out? I think if you move that blue column over there, I'll be able to make it across.",
        "Press the action button when you're nearby to push it, and when you think I can make it, press X and I'll give it a shot."
    };

    void ITriggerable.Trigger()
    {
        playerCamera.FocusOnObject(MainObjectContainer.Instance.Player2D, new Vector3(0, -2, 3));
        MasterMonoBehaviour.Instance.DisplayMessage(introductionText, () => {
            playerCamera.RestoreFocus();
        });
    }
}
