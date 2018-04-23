using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIntroduction : MonoBehaviour, ITriggerable {
    public PlayerCamera playerCamera;

    int textNumber = 0;

    private static string[] introductionText1 = new string[] {
        "Hello there! My name is Echo. I'm trying to get home, but I can't seem to make the jump across these blocks.",
        "Maybe you can help me out? I think if you move that blue column over there, I'll be able to make it across.",
        "Press the action button when you're nearby to push it, and when you think I can make it, press X and I'll give it a shot."
    };

    private static string[] introductionText2 = new string[] {
        "Great work! I think this partnership could work out nicely.",
        "Just don't forget to read the signs if you ever get stuck..."
    };

    private static string[][] introTexts = new string[][] { introductionText1, introductionText2 };

    void ITriggerable.Trigger()
    {
        PlayerFreezeHandler.TriggerPlayerFreeze();
        playerCamera.FocusOnObject(MainObjectContainer.Instance.Player2D, new Vector3(0, -2, 3));
        MasterMonoBehaviour.Instance.DisplayMessage(introTexts[Mathf.Min(textNumber, introTexts.Length)], () => {
            textNumber++;
            playerCamera.RestoreFocus();
            PlayerFreezeHandler.TriggerPlayerUnfreeze();
        });
    }
}
