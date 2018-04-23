using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneController : MonoBehaviour {
    void Start () {
        GameMainState.CheckForLevelIntegrity();
        MasterStateMachine.Instance.setState(new IntroState());
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.F1))
            ScreenCapture.CaptureScreenshot("Assets/Screenshots/Shot1.png");
    }
}
