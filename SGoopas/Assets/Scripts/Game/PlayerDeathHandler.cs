using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler {
    public delegate void PlayerDeathOccurred();
    public static event PlayerDeathOccurred PlayerDeathEvent;
    public static void TriggerPlayerDeath() {
        PlayerDeathEvent();
    }
    public static void ResetDeathEvent() {
        PlayerDeathEvent = null;
    }
}
