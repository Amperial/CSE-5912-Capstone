using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MasterMonoBehaviour : MonoBehaviour {
    public static MasterMonoBehaviour Instance;
	public GameObject messageContainer;
	private TextAnimator animator;
	public GameObject FaderHolder;
	private Fader fader;
	public GameObject pauseMenu;
    void Awake()
    {
        Instance = this;
		animator = messageContainer.GetComponent<TextAnimator> ();
		fader = FaderHolder.GetComponent<Fader> ();
		pauseMenu.SetActive (false);
    }

	public void DisplayMessage(string[] messageLines){
		animator.init (messageLines);
		messageContainer.SetActive (true);
		animator.enabled = true;
	}

	public void TerminateMessage(){
		animator.enabled = false;
		messageContainer.SetActive (false);
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
}
