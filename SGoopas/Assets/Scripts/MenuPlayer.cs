using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuPlayer : MonoBehaviour {
	public static string selection = "Menu Play Shadow";
	public AudioSource buttonPress;
	public Text playText, creditsText, quitText, levelsText;
	private static MenuPlayer Instance;
	private Color selected, unselected;

	// Use this for initialization
	void Start () {
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
		Instance = this;
		if (MasterMonoBehaviour.Instance != null) {
			MasterMonoBehaviour.Instance.SetFade (1f);
			MasterMonoBehaviour.Instance.FadeScreen (0f);
		}

		unselected = new Color (.2f, .2f, .2f);
		selected = new Color (.9f, .2f, .2f);
	}

	void OnCollisionEnter2D(Collision2D other){
		selection = other.gameObject.name;

		quitText.color = unselected;
		creditsText.color = unselected;
		playText.color = unselected;
		levelsText.color = unselected;

		switch (selection) {
		case "Menu Play Shadow":
			playText.color = selected;
			break;
		case "Menu Level Select Shadow":
			levelsText.color = selected;
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
			Instance.LevelSelect ();
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
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.GoToFirstLevel();
		});
	}

	public void LevelSelect()
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.GoToLevelSelect();
		});
	}

	public void Credits()
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.setState(new CreditsState());
		});
	}

	public void Quit()
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.setState(new QuitState());
		});
	}
}
