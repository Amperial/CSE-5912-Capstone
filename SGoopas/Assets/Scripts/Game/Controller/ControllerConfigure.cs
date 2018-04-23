using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class ControllerConfigure : MonoBehaviour {
    private MasterPlayerStateMachine playerStateMachine;
    private Controller controller;

	public bool mainMenu = false;
    public MasterPlayerStateMachine PlayerStateMachine
    {
        get
        {
            return playerStateMachine;
        }
    }

    private void ConfigureControls()
    {
        controller.RegisterButtonDown("Jump", playerStateMachine.Jump);
        controller.RegisterButtonDown("Action", playerStateMachine.Action);
        controller.RegisterAxis("Horizontal", playerStateMachine.MoveLeft, playerStateMachine.MoveRight);
        controller.RegisterAxis("Vertical", playerStateMachine.MoveDown, playerStateMachine.MoveUp);
        controller.RegisterButtonDown("Release", playerStateMachine.Release);
        controller.RegisterButtonDown("Reset", MasterStateMachine.Instance.ResetLevel);
		if (mainMenu) {
			controller.RegisterButtonDown ("Submit", MenuPlayer.MenuSelect);
		} else {
            controller.RegisterButtonDown ("SwapDimension", playerStateMachine.DimensionSwapButtonPressed);
		}
    }

	// Use this for initialization
	void Start () {
        controller = new Controller();
        playerStateMachine = new MasterPlayerStateMachine(MainObjectContainer.Instance.Player2D, MainObjectContainer.Instance.Player3D);
        ConfigureControls();

        if (mainMenu) {
            playerStateMachine.DimensionSwapButtonPressed();
		}
    }
	
	// Update is called once per frame
	void Update () {
        controller.Update();
        playerStateMachine.Update();
    }

    void FixedUpdate()
    {
        controller.FixedUpdate();
        playerStateMachine.FixedUpdate();
    }

    void LateUpdate()
    {
        playerStateMachine.LateUpdate();
    }
}
