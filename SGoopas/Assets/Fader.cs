using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fader : MonoBehaviour {
	public float fadeSpeed = 1.0f;
	private bool isFading = false;
	public GameObject imageHolder;
	private float fadeTarget = 0f;
	private Image faderScreen;
	private Action faderCallback;

	// Use this for initialization
	void Start () {
		faderScreen = imageHolder.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update(){
		if (isFading) {
			float fadeChange = (fadeSpeed * Time.deltaTime) * Mathf.Sign(fadeTarget - faderScreen.color.a);
			float newAlpha = faderScreen.color.a + fadeChange;
			if ((fadeChange > 0 && newAlpha > fadeTarget)
				|| fadeChange < 0 && newAlpha < fadeTarget) {
				newAlpha = fadeTarget;
				isFading = false;
				if (faderCallback != null) {
					faderCallback ();
				}
			}
			faderScreen.color = new Color (faderScreen.color.r, faderScreen.color.g, faderScreen.color.b, newAlpha);
		}
	}

	public void FadeScreen(float target, Action action = null){
		isFading = true;
		fadeTarget = target;
		faderCallback = action;
	}

	public void SetFade(float a){
		faderScreen.color = new Color (faderScreen.color.r, faderScreen.color.g, faderScreen.color.b, a);
	}
}
