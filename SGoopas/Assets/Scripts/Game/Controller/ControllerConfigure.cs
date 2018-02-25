using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class ControllerConfigure : MonoBehaviour {
    public GameObject player2D;
    public GameObject player3D;
    private MasterPlayerStateMachine playerStateMachine;
    private Controller controller;

    public static bool is2D = false;
    private static GameObject gameSingleton;

    void Awake()
    {
        gameSingleton = gameObject;
    }

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
        playerStateMachine.SwitchDimension();
        Cancellable cancellable = new Cancellable();
        BroadcastMessage(is2D ? "SwitchTo3D" : "SwitchTo2D", cancellable, SendMessageOptions.DontRequireReceiver);
        if (!cancellable.IsCancelled)
        {
            is2D = !is2D;
        }
    }

    private void ConfigureControls()
    {
        controller.RegisterButton("Jump", playerStateMachine.Jump);
        controller.RegisterButton("Action", playerStateMachine.Action);
        controller.RegisterButtonDown("SwapDimension", SwapDimension);
        controller.RegisterAxis("Horizontal", playerStateMachine.MoveLeft, playerStateMachine.MoveRight);
        controller.RegisterAxis("Vertical", playerStateMachine.MoveDown, playerStateMachine.MoveUp);
        controller.RegisterButton("Release", playerStateMachine.Release);
    }

	// Use this for initialization
	void Start () {
        controller = new Controller();
        playerStateMachine = new MasterPlayerStateMachine(player2D, player3D);
        ConfigureControls();
    }
	
	// Update is called once per frame
	void Update () {
        playerStateMachine.Update();
    }

    void FixedUpdate()
    {
        controller.Update();
        playerStateMachine.FixedUpdate();
    }
}
