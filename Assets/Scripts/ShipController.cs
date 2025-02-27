using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ShipController : MonoBehaviour
{
    private CharacterController characterController;
    private InputHandler inputHandler;

    [Header("Movement Speeds")]
    [SerializeField] private float shipSpeed = 5.0f;
    [SerializeField] private float boostMultiplier = 2.0f;

    [Header("Rotation Settings")]
    [SerializeField] private float turnSpeed = 90f; // Degrees per second
    [SerializeField] private float smoothTime = 0.1f; // Smooth dampening factor

    private float currentYaw = 0f;  // Current rotation
    private float yawVelocity = 0f; // Used for turn smoothing

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = InputHandler.Instance;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speed = shipSpeed * (inputHandler.SprintValue > 0 ? boostMultiplier : 1f);
        Vector3 moveDirection = transform.forward * speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleRotation()
    {
        float clampedInput = Mathf.Clamp(inputHandler.TurnInput, -1, 1);

        // Calculate target yaw (turnSpeed determines how fast it turns)
        float targetYaw = currentYaw + (clampedInput * turnSpeed * Time.deltaTime);

        // Smoothly interpolate toward the target rotation
        currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, smoothTime);

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, currentYaw, 0);
    }
}
