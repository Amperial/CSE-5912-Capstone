using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneController : MonoBehaviour {
	void Start () {
        MasterStateMachine.Instance.setState(new IntroState());
    }
}
