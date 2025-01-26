using UnityEngine;
using UnityEngine.InputSystem; // New Input System
using System; // For Action<>

public class ControllerInputAdapter : MonoBehaviour, IInputAdapter
{
    public event Action<InputEvent> OnInputReceived;

    private InputAction lbAction;
    private InputAction rbAction;

    private InputActionMap inputActionMap;

    void Start()
    {
        // Initialize a new input action map
        inputActionMap = new InputActionMap("Gamepad");

        // Define LB and RB actions
        lbAction = inputActionMap.AddAction("LB", binding: "<Gamepad>/leftShoulder");
        rbAction = inputActionMap.AddAction("RB", binding: "<Gamepad>/rightShoulder");

        // Enable the input actions
        inputActionMap.Enable();

        gameObject.GetComponent<InputManager>().RegisterAdapter(this);
    }

    public void Listen()
    {
        // Update gamepad input state manually if needed (optional)
        Gamepad gamepad = Gamepad.current;

        if (gamepad == null) return; // No controller connected

        // Detect LB and RB manually if you'd like additional control
        if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            TriggerEvent("Left");
        }

        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            TriggerEvent("Right");
        }
    }

    private void TriggerEvent(string inputName)
    {
        OnInputReceived?.Invoke(new InputEvent
        {
            InputName = inputName,
            AdapterName = "Controller",
        });
    }

    void OnDestroy()
    {
        // Dispose of actions to clean up resources
        if (lbAction != null) lbAction.Dispose();
        if (rbAction != null) rbAction.Dispose();
        if (inputActionMap != null) inputActionMap.Disable();
    }
}
