using System.Collections;
using System.Collections.Generic;

public class DimensionSwitchHandler
{
    public delegate void DimensionSwitchOccurred();
    public static event DimensionSwitchOccurred DimensionSwitchEvent;
    public static void TriggerDimensionSwitch()
    {
        DimensionSwitchEvent();
    }
    public static void ResetDimensionSwitchEvent()
    {
        DimensionSwitchEvent = null;
    }
}
