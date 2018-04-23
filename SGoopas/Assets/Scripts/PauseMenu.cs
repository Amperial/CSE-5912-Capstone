using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public void Unpause () {
		MasterStateMachine.Instance.unpause();
	}

    public void ResetLevel() {
        Unpause();
        MasterStateMachine.Instance.ResetLevel();
    }

    public void NextLevel() {
        Unpause();
        MasterStateMachine.Instance.GoToNextLevel();
    }

    public void GotoMainMenu () {
        MasterStateMachine.Instance.unpause();
		MasterMonoBehaviour.Instance.FadeScreen (1, () => {
			MasterStateMachine.Instance.setState(new MainMenuState());
		});
    }
}
