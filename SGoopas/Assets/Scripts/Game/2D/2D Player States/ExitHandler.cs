using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler
{
    public delegate void ExitLevel();
    public static event ExitLevel ExitEvent;
    public static void TriggerExit()
    {
        if(ExitEvent != null) {
            ExitEvent();
        }
    }
    public static void ResetExitEvent()
    {
        ExitEvent = null;
    }
}
