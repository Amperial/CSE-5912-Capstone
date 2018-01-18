using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : IGameState
{
    void IGameState.onEnter()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    void IGameState.onExit()
    {
        SceneManager.UnloadSceneAsync(4);
    }
}
