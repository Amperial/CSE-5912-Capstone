using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneController : MonoBehaviour {
    private int delay;
	void Start () {
        MasterStateMachine.Instance.setState(new IntroState());
        delay = 0;
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.F1))
            ScreenCapture.CaptureScreenshot("Assets/Screenshots/Shot1.png");
        if (Input.GetKey(KeyCode.Escape))
        {
            if (MasterStateMachine.Instance.getState() is GameMainState && delay == 0)
            {
                delay = 10;
                if (MasterStateMachine.Instance.getPaused())
                    MasterStateMachine.Instance.unpause();
                else
                    MasterStateMachine.Instance.pause();
            }
        }
        if (delay > 0)
            delay--;

    }
}
