using PlayerStates;
using UnityEngine;

public class ControllerConfigure : MonoBehaviour {
    public GameObject player2D;
    public GameObject player3D;
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

    private void ConfigureControls()
    {
        controller.RegisterButtonDown("Jump", playerStateMachine.Jump);
        controller.RegisterButtonDown("Action", playerStateMachine.Action);
        controller.RegisterButtonDown("SwapDimension", playerStateMachine.SwitchDimension);
        controller.RegisterAxis("Horizontal", playerStateMachine.MoveLeft, playerStateMachine.MoveRight);
        controller.RegisterAxis("Vertical", playerStateMachine.MoveDown, playerStateMachine.MoveUp);
        controller.RegisterButtonDown("Release", playerStateMachine.Release);
    }

    void Awake() {
        controller = new Controller();
        playerStateMachine = new MasterPlayerStateMachine(player2D, player3D);
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

    void OnEnable()
    {
        DimensionControl.OnSwitchDimension += playerStateMachine.OnSwitchDimension;
    }

    void OnDisable()
    {
        DimensionControl.OnSwitchDimension -= playerStateMachine.OnSwitchDimension;
    }
}
