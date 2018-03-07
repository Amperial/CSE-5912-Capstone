using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public AudioSource buttonPress;
	public void Play()
    {
        buttonPress.Play();
        MasterStateMachine.Instance.GoToFirstLevel();
    }

    public void Spotlight()
    {
        buttonPress.Play();
        MasterStateMachine.Instance.GoToSpotlightLevel();
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
