using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayer : MonoBehaviour {
	public static string selection = "Menu Play Shadow";
	public AudioSource buttonPress;
	public Text playText, creditsText, quitText;
	private static MenuPlayer Instance;
	private Color selected, unselected;

	// Use this for initialization
	void Start () {
		Instance = this;
		unselected = new Color (.2f, .2f, .2f);
		selected = new Color (.9f, .2f, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		selection = other.gameObject.name;

		quitText.color = unselected;
		creditsText.color = unselected;
		playText.color = unselected;

		switch (selection) {
		case "Menu Play Shadow":
			playText.color = selected;
			break;
		case "Menu Credits Shadow":
			creditsText.color = selected;
			break;
		case "Menu Quit Shadow":
			quitText.color = selected;
			break;
		}
	}

	public static void MenuSelect(){
		switch (selection) {
		case "Menu Play Shadow":
			Instance.Play ();
			break;
		case "Menu Level Select Shadow":
			Debug.Log ("level select");
			break;
		case "Menu Credits Shadow":
			Instance.Credits ();
			break;
		case "Menu Quit Shadow":
			Instance.Quit ();
			break;
		}
	}

	public void Play()
	{
		buttonPress.Play();
        MasterStateMachine.Instance.GoToFirstLevel();
	}

	public void Credits()
	{
		buttonPress.Play();
        MasterStateMachine.Instance.setState(new CreditsState());
	}

	public void Quit()
	{
		buttonPress.Play();
        MasterStateMachine.Instance.setState(new QuitState());
	}
}
