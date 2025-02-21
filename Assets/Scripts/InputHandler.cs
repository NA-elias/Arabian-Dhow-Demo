// === === ===
// This script controls which input actions maps to enable/ disable.
// The script also reads inputs triggered and returns the corresponding values.
// 
// ~ Elias
// === === ===

// TODO - During later stages of development, it may be required to split [Player] action map 
// TODO - and [UI] action map into different scripts and make this script as the Main input controller.
// The following video is a neat script that was used as reference [Source](https://youtu.be/lclDl-NGUMg?si=NZE1PZ8_7wlB4qLb)

using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private enum PlayerAction
    {
        Move,
        Look,
        Interact,
        Sprint,
    }

    private GameControlScheme gameControlScheme;

    public static InputHandler Instance { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractionTriggered { get; private set; }
    public float SprintValue { get; private set; }

    void OnEnable()
    {
        gameControlScheme.Enable();
        InputSystem.onDeviceChange += PrintDevicesConnectedStatus;
    }

    void OnDisable()
    {
        gameControlScheme.Disable();
        InputSystem.onDeviceChange -= PrintDevicesConnectedStatus;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameControlScheme = new GameControlScheme();

        SubscribeInputEvents();
        printDevicesConnected();
    }

    private void SubscribeInputEvents()
    {
        gameControlScheme.Player.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        gameControlScheme.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        gameControlScheme.Player.Look.performed += context => LookInput = context.ReadValue<Vector2>();
        gameControlScheme.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        gameControlScheme.Player.Interact.performed += _ => InteractionTriggered = true;
        gameControlScheme.Player.Interact.performed += _ => InteractionTriggered = false;

        gameControlScheme.Player.Sprint.performed += context => SprintValue = context.ReadValue<float>();
        gameControlScheme.Player.Sprint.canceled += _ => SprintValue = 0f;
    }

    void printDevicesConnected()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log("Active Device: " + device.name);
            }
        }
    }

    void PrintDevicesConnectedStatus(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                Debug.Log("Device Disconnected: " + device.name);
                // ? Handle disconnection (if required)
                break;
            case InputDeviceChange.Reconnected:
                Debug.Log("Device Reconnected: " + device.name);
                // ? Handle reconnection (if required)
                break;
        }
    }
}
