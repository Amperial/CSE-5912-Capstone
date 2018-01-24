using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainState : IGameState
{
    private const string sceneName = "GameMain";

    /*
     * Load this scene asynchronously.
     */
    public AsyncOperation loadAsynchronously()
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    void IGameState.onEnter()
    {
        // No-op, this scene should be loaded asynchronously.
    }

    void IGameState.onExit()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    void IGameState.onPause()
    {
        SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].SetActive(false);
    }

    void IGameState.onUnpause()
    {
        SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].SetActive(true);
    }
}
