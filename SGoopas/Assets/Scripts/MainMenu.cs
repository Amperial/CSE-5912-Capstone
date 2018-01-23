using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTxt;
    public AudioSource buttonPress;

    void Start()
    {
        loadScreen.SetActive(false);
    }
	public void Play(int level)
    {
        buttonPress.Play();
        StartCoroutine(LoadLevel(level));
    }

    public void Credits()
    {
        buttonPress.Play();
        MasterStateMachine.Instance.setState(new CreditsState());
    }

    public void Quit()
    {
        buttonPress.Play();
        Application.Quit();
    }

    private void PlaySound() {
        buttonPress.Play();
    }

    IEnumerator LoadLevel(int level)
    {
        IGameState loading;
        loading = new PongState();

        float progress;
        AsyncOperation operation = MasterStateMachine.Instance.load(loading);
        loadScreen.SetActive(true);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);

            Debug.Log(operation.progress);
            slider.value = progress;
            progressTxt.text = progress * 100f + "%";

            yield return null;
        }
        MasterStateMachine.Instance.setState(new PongState());
    }
}
