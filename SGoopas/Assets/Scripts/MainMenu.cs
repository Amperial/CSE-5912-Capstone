﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTxt;

    void Start()
    {
        loadScreen.SetActive(false);
    }
	public void Play(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    public void Credits()
    {
        MasterStateMachine.Instance.setState(new CreditsState());
    }

    public void Quit()
    {
        MasterStateMachine.Instance.setState(new QuitState());
    }

    IEnumerator LoadLevel(int level)
    {
        PongState loading;
        loading = new PongState();

        float progress;
        AsyncOperation operation = loading.loadAsynchronously();
        loadScreen.SetActive(true);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressTxt.text = progress * 100f + "%";

            yield return null;
        }
        MasterStateMachine.Instance.setState(loading);
    }
}
