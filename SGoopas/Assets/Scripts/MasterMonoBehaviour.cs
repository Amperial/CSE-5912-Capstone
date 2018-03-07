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

    void Awake()
    {
        Instance = this;
		animator = messageContainer.GetComponent<TextAnimator> ();
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
}
