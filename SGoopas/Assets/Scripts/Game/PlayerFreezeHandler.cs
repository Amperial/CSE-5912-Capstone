using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezeHandler
{
    public delegate void PlayerFreezeOccurred();
    public static event PlayerFreezeOccurred PlayerFreezeEvent;
    public static void TriggerPlayerFreeze()
    {
        PlayerFreezeEvent();
    }
    public static void ResetFreezeEvent()
    {
        PlayerFreezeEvent = null;
    }

    public delegate void PlayerUnfreezeOccurred();
    public static event PlayerUnfreezeOccurred PlayerUnfreezeEvent;
    public static void TriggerPlayerUnfreeze()
    {
        PlayerUnfreezeEvent();
    }
    public static void ResetUnfreezeEvent()
    {
        PlayerUnfreezeEvent = null;
    }
}
