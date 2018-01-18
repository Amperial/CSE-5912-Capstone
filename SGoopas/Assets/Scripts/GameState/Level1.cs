using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : IGameState
{
    void IGameState.onEnter()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    void IGameState.onExit()
    {
        SceneManager.UnloadSceneAsync(3);
    }
}
