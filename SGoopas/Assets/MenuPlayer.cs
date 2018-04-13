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
		if (MasterMonoBehaviour.Instance != null) {
			MasterMonoBehaviour.Instance.SetFade (1f);
			MasterMonoBehaviour.Instance.FadeScreen (0f);
		}

		unselected = new Color (.2f, .2f, .2f);
		selected = new Color (.9f, .2f, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		selection = other.gameObject.name;
		switch (selection) {
		case "Menu Play Shadow":
			playText.color = selected;
			creditsText.color = unselected;
			quitText.color = unselected;
			break;
		case "Menu Credits Shadow":
			creditsText.color = selected;
			quitText.color = unselected;
			playText.color = unselected;
			break;
		case "Menu Quit Shadow":
			quitText.color = selected;
			creditsText.color = unselected;
			playText.color = unselected;
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
			//Credits ();
			break;
		case "Menu Quit Shadow":
			//Quit ();
			break;
		}
	}

	public void Play()
	{
		//buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.GoToFirstLevel();
			MasterMonoBehaviour.Instance.FadeScreen (0f);
		});
	}

	public void Spotlight()
	{
		buttonPress.Play();
		MasterStateMachine.Instance.GoToSpotlightLevel();
	}

	public void Credits()
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.setState(new CreditsState());
			MasterMonoBehaviour.Instance.FadeScreen (0f);
		});
	}

	public void Quit()
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.setState(new QuitState());
			MasterMonoBehaviour.Instance.FadeScreen (0f);
		});
	}
}
