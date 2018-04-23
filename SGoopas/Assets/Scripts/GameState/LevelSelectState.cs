using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectState : IGameState {
	private const string sceneName = "LevelSelect";

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
