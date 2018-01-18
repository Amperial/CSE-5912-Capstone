using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneController : MonoBehaviour {

    private static MasterSceneController instance;
    IGameState intro, level1, menu, credits, currState;

    private MasterSceneController() { }

    public static MasterSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MasterSceneController();
            }
            return instance;
        }
    }
    
	void Start () {
        intro = new Intro();

        currState = intro;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
	

    public void setState(IGameState state)
    {
        currState.onExit();
        currState = state;
        currState.onEnter();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
