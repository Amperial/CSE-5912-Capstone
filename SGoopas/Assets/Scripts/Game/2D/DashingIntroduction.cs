using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingIntroduction : MonoBehaviour, ITriggerable
{
    public PlayerCamera playerCamera;

    int textNumber = 0;

    private static string[] introductionText1 = new string[] {
        "Oh no! My Knight is so far away and there are these big, scary, shadow creatures! What can I do?",
        "Well, my grandmother used to always say \"Echo, if things get rough, just take ACTION and ZOOM through life's problems\"",
        "I never really understood her, but if ever there were a time to figure it out, it would be now."
    };

    private static string[] introductionText2 = new string[] {
        "Did you see what I did back there?!",
        "I moved so fast! And I felt invincible!",
        "After I beat one of those shadow monsters, I felt so confident, I bet I could've ZOOMed again without even touching the ground!",
        "Maybe I can use this power to reach my Knight!"
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
