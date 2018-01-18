using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : IGameState {
    public void onEnter()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void onExit()
    {
        SceneManager.UnloadSceneAsync(2);
    }

}
