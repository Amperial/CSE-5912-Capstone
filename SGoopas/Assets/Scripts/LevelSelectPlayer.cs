using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelectPlayer : MonoBehaviour {
	public static string selection = "Menu Level 1 Shadow";
	public AudioSource buttonPress;
	public Text backText, level1Text, level2Text, level3Text, level4Text, level5Text, level6Text;
	private static LevelSelectPlayer Instance;
	private Color selected, unselected;

	// Use this for initialization
	void Start () {
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelSelect"));
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

		backText.color = unselected;
		level1Text.color = unselected;
		level2Text.color = unselected;
		level3Text.color = unselected;
		level4Text.color = unselected;
		level5Text.color = unselected;
        level6Text.color = unselected;

        switch (selection) {
		case "Menu Back Shadow":
			backText.color = selected;
			break;
		case "Menu Level 1 Shadow":
			level1Text.color = selected;
			break;
		case "Menu Level 2 Shadow":
			level2Text.color = selected;
			break;
		case "Menu Level 3 Shadow":
			level3Text.color = selected;
			break;
		case "Menu Level 4 Shadow":
			level4Text.color = selected;
			break;
		case "Menu Level 5 Shadow":
			level5Text.color = selected;
                break;
        case "Menu Level 6 Shadow":
                level6Text.color = selected;
                break;
		}
	}

	public static void LevelSelect(){
		switch (selection) {
		case "Menu Back Shadow":
			Instance.MainMenu ();
			break;
		case "Menu Level 1 Shadow":
			Instance.GoToLevel (0);
			break;
		case "Menu Level 2 Shadow":
			Instance.GoToLevel (1);
			break;
		case "Menu Level 3 Shadow":
			Instance.GoToLevel (2);
			break;
		case "Menu Level 4 Shadow":
			Instance.GoToLevel (3);
			break;
		case "Menu Level 5 Shadow":
			Instance.GoToLevel (4);
			break;
        case "Menu Level 6 Shadow":
            Instance.GoToLevel(5);
            break;
        }
	}

	public void MainMenu(){
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1, () => {
			MasterStateMachine.Instance.setState(new MainMenuState());
		});
	}

	public void GoToLevel(int level)
	{
		buttonPress.Play();
		MasterMonoBehaviour.Instance.FadeScreen (1f, () => {
			MasterStateMachine.Instance.GoToLevel(level);
		});
	}
		
}
