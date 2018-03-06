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

    public void GoToFirstLevel() {
        MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(new GameMainState()));
    }

    public void GoToNextLevel() {
        if (currentState is GameMainState) {
            GameMainState newLevel = ((GameMainState)currentState).GetStateForNextLevel();
            MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(newLevel));
        }
    }

    public IEnumerator LoadLevelAsynchronously(GameMainState loading) {
        float progress;
        AsyncOperation operation = loading.loadAsynchronously();
        MasterMonoBehaviour.Instance.loadScreen.SetActive(true);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            MasterMonoBehaviour.Instance.slider.value = progress;
            MasterMonoBehaviour.Instance.progressTxt.text = progress * 100f + "%";
            yield return null;
        }
        setState(loading);
        loading.SetAsActiveScene();
        MasterMonoBehaviour.Instance.loadScreen.SetActive(false);
    }
}
