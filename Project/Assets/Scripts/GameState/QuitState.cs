using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitState : IGameState {
    public void onEnter()
    {
        Application.Quit();
    }

    public void onExit()
    {
        // No-op.
    }

    public void onPause()
    {
        // No-op.
    }

    public void onUnpause()
    {
        // No-op.
    }
}
