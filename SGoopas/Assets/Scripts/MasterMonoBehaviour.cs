using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MasterMonoBehaviour : MonoBehaviour {
    public static MasterMonoBehaviour Instance;
	public GameObject messageContainer;
	private TextAnimator animator;
    private System.Action messageCloseAction;
	public GameObject FaderHolder;
	private Fader fader;
	public GameObject pauseMenu;
    public AudioSource audioSource;

    void Awake()
    {
        Instance = this;
		animator = messageContainer.GetComponent<TextAnimator> ();
		fader = FaderHolder.GetComponent<Fader> ();
		pauseMenu.SetActive (false);
    }

	public void DisplayMessage(string[] messageLines){
        DisplayMessage(messageLines, () => {});
	}

    public void DisplayMessage(string[] messageLines, System.Action onMessageClosed)
    {
        animator.init(messageLines);
        messageContainer.SetActive(true);
        animator.enabled = true;
        messageCloseAction = onMessageClosed;
    }

	public void TerminateMessage(){
		animator.enabled = false;
		messageContainer.SetActive (false);
        messageCloseAction();
	}

	public void FadeScreen(float target, Action action = null){
		fader.FadeScreen (target, action);
	}

	public void SetFade(float a){
		fader.SetFade (a);
	}

	public void ShowPauseMenu(){
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void HidePauseMenu(){
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

    public void Click()
    {
        audioSource.Play();
    }
}
