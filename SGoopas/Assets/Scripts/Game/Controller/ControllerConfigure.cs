using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class ControllerConfigure : MonoBehaviour {
    private MasterPlayerStateMachine playerStateMachine;
    private Controller controller;

    public bool is2D = false;

    public MasterPlayerStateMachine PlayerStateMachine
    {
        get
        {
            return playerStateMachine;
        }
    }
    /**
     * To be replaced with an event-based swap in the future
     */
    public void SwapDimension()
    {
        Cancellable cancellable = new Cancellable();
        cancellable.PerformCancellable(playerStateMachine.SwitchDimension, playerStateMachine.SwitchDimension);
        
        BroadcastMessage(is2D ? "SwitchTo3D" : "SwitchTo2D", cancellable, SendMessageOptions.DontRequireReceiver);
        if (!cancellable.IsCancelled)
        {
            is2D = !is2D;
        }
    }

    private void ConfigureControls()
    {
        controller.RegisterButtonDown("Jump", playerStateMachine.Jump);
        controller.RegisterButtonDown("Action", playerStateMachine.Action);
        controller.RegisterButtonDown("SwapDimension", SwapDimension);
        controller.RegisterAxis("Horizontal", playerStateMachine.MoveLeft, playerStateMachine.MoveRight);
        controller.RegisterAxis("Vertical", playerStateMachine.MoveDown, playerStateMachine.MoveUp);
        controller.RegisterButtonDown("Release", playerStateMachine.Release);
    }

	// Use this for initialization
	void Start () {
        controller = new Controller();
        playerStateMachine = new MasterPlayerStateMachine(MainObjectContainer.Instance.Player2D, MainObjectContainer.Instance.Player3D);
        ConfigureControls();
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
