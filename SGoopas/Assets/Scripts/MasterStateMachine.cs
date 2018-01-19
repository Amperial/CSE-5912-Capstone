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
        if (currentState != null && !(state is PausedState))
        {
            currentState.onExit();
        }
        currentState = state;
        currentState.onEnter();
    }
}
