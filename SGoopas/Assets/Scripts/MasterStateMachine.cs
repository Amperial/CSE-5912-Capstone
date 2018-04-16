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
            if (MasterMonoBehaviour.Instance != null)
            {
                isPaused = true;
                currentState.onPause();
                MasterMonoBehaviour.Instance.ShowPauseMenu();
            }
        }
    }

    public void unpause() {
        if (isPaused)
        {
            if (MasterMonoBehaviour.Instance != null)
            {
                isPaused = false;
                currentState.onUnpause();
                MasterMonoBehaviour.Instance.HidePauseMenu();
            }
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

    public void ResetLevel() {
        if (currentState is GameMainState)
        {
            MasterMonoBehaviour.Instance.StartCoroutine(ReloadLevelAsynchronously());
        }
    }

    public void GoToSpotlightLevel()
    {
        MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(new GameMainState(2)));
    }

    private IEnumerator UpdateLoadingWithProgress(AsyncOperation operation)
    {
        float progress;
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            MasterMonoBehaviour.Instance.slider.value = progress;
            MasterMonoBehaviour.Instance.progressTxt.text = progress * 100f + "%";
            yield return null;
        }
    }

    public IEnumerator LoadLevelAsynchronously(GameMainState loading) {
        MasterMonoBehaviour.Instance.loadScreen.SetActive(true);
        MainObjectContainer.Reset();
        AsyncOperation operation = loading.loadAsynchronously();
        yield return UpdateLoadingWithProgress(operation);
        setState(loading);
        loading.SetAsActiveScene();
        MasterMonoBehaviour.Instance.loadScreen.SetActive(false);
    }

    public IEnumerator ReloadLevelAsynchronously()
    {
        MasterMonoBehaviour.Instance.loadScreen.SetActive(true);
        MainObjectContainer.Reset();
        currentState.onExit();
        GameMainState loading = (GameMainState)currentState;
        AsyncOperation operation = loading.loadAsynchronously();
        yield return UpdateLoadingWithProgress(operation);
        loading.SetAsActiveScene();
        MasterMonoBehaviour.Instance.loadScreen.SetActive(false);
    }
}
