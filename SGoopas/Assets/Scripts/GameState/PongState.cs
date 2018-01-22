using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongState : IGameState
{
    private const string sceneName = "Pong";

    AsyncOperation IGameState.load()
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    void IGameState.onEnter()
    {
        
    }

    void IGameState.onExit()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    void IGameState.onPause()
    {
        // No-op for now, will eventually freeze items within pong.
    }

    void IGameState.onUnpause()
    {
        // No-op for now, will eventually unfreeze items within the game.
    }
}
