using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : IGameState {
    private const string sceneName = "MainMenu";

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
