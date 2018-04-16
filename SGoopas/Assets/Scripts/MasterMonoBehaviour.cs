using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterMonoBehaviour : MonoBehaviour {
    public static MasterMonoBehaviour Instance;
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTxt;
	public GameObject messageContainer;
	private TextAnimator animator;
    public GameObject pauseMenu;

    void Awake()
    {
        Instance = this;
		animator = messageContainer.GetComponent<TextAnimator> ();
        pauseMenu.SetActive(false);
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

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
