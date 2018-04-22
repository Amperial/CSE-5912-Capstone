﻿using System.Collections;
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
			if (MasterMonoBehaviour.Instance != null) {
				MasterMonoBehaviour.Instance.FadeScreen (.4f, () => {
					MasterMonoBehaviour.Instance.ShowPauseMenu ();
				});
			}
        }
    }

    public void unpause() {
        if (isPaused)
        {
            isPaused = false;
            currentState.onUnpause();
			if (MasterMonoBehaviour.Instance != null) {
				MasterMonoBehaviour.Instance.HidePauseMenu ();
				MasterMonoBehaviour.Instance.FadeScreen (0f);
			}
		}
    }

    public void GoToFirstLevel() {
        MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(new GameMainState()));
    }

	public void GoToLevelSelect(){
		//MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(new GameMainState(5)));
		MasterStateMachine.Instance.setState(new LevelSelectState());
	}

    public void GoToNextLevel() {
        if (currentState is GameMainState) {
            GameMainState newLevel = ((GameMainState)currentState).GetStateForNextLevel();
            MasterMonoBehaviour.Instance.FadeScreen(1f, () => {
                MasterMonoBehaviour.Instance.StartCoroutine(LoadLevelAsynchronously(newLevel));
            });
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
        while (!operation.isDone)
        {
            yield return null;
        }
        MasterMonoBehaviour.Instance.FadeScreen(0);
    }

    public IEnumerator LoadLevelAsynchronously(GameMainState loading) {
        MainObjectContainer.Reset();
        AsyncOperation operation = loading.loadAsynchronously();
        yield return UpdateLoadingWithProgress(operation);

		setState(loading);
		loading.SetAsActiveScene();
    }

    public IEnumerator ReloadLevelAsynchronously()
    {
        MainObjectContainer.Reset();
        currentState.onExit();
        GameMainState loading = (GameMainState)currentState;
        AsyncOperation operation = loading.loadAsynchronously();
        yield return UpdateLoadingWithProgress(operation);

		loading.SetAsActiveScene();
    }
}
