using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroState : IGameState
{
    private const string sceneName = "Intro";

    void IGameState.onEnter()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    void IGameState.onExit()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    void IGameState.onPause()
    {
        // No-op.
    }

    void IGameState.onUnpause()
    {
        // No-op.
    }
}
