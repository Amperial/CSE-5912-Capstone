using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Basic game state machine singleton.
 */
public class MasterStateMachine
{
    private static MasterStateMachine instance;
    private const string pauseScene = "Paused";
    private bool isPaused = false;
    IGameState currentState;

    private MasterStateMachine() { }

    public static MasterStateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MasterStateMachine();
            }
            return instance;
        }
    }

    public void setState(IGameState state)
    {
        if (currentState != null)
        {
            currentState.onExit();
        }
        currentState = state;
        currentState.onEnter();
    }

    public void pause() {
        if (!isPaused)
        {
            isPaused = true;
            currentState.onPause();
            SceneManager.LoadScene(pauseScene, LoadSceneMode.Additive);
        }
    }

    public void unpause() {
        if (isPaused)
        {
            isPaused = false;
            currentState.onUnpause();
            SceneManager.UnloadSceneAsync(pauseScene);
        }
    }
}
