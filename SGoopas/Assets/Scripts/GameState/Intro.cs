using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : IGameState
{
    void IGameState.onEnter()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    void IGameState.onExit()
    {
        //SceneManager.UnloadSceneAsync(1);
    }
}
