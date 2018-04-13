using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MasterMonoBehaviour : MonoBehaviour {
    public static MasterMonoBehaviour Instance;
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTxt;
	public GameObject messageContainer;
	private TextAnimator animator;
	public GameObject imageHolder;
	private bool isFading = false;
	private float fadeTarget = 0f;
	private Image fader;
	public float fadeSpeed;
	private Action faderCallback;
	public GameObject pauseMenu;
    void Awake()
    {
        Instance = this;
		animator = messageContainer.GetComponent<TextAnimator> ();
		fader = imageHolder.GetComponent<Image> ();
		pauseMenu.SetActive (false);
    }

	void Update(){
		if (isFading) {
			float fadeChange = (fadeSpeed * Time.deltaTime) * Mathf.Sign(fadeTarget - fader.color.a);
			float newAlpha = fader.color.a + fadeChange;
			if ((fadeChange > 0 && newAlpha > fadeTarget)
				|| fadeChange < 0 && newAlpha < fadeTarget) {
				newAlpha = fadeTarget;
				isFading = false;
				if (faderCallback != null) {
					faderCallback ();
				}
			}
			fader.color = new Color (fader.color.r, fader.color.g, fader.color.b, newAlpha);
		}
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
		isFading = true;
		fadeTarget = target;
		faderCallback = action;
	}

	public void SetFade(float a){
		fader.color = new Color (fader.color.r, fader.color.g, fader.color.b, a);
	}

	public void ShowPauseMenu(){
		pauseMenu.SetActive (true);
	}

	public void HidePauseMenu(){
		pauseMenu.SetActive (false);
	}
}
