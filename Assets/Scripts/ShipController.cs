using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float shipSpeed = 5.0f;
    [SerializeField] private float boostMultiplier = 2.0f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;

    private CharacterController characterController;
    private InputHandler inputHandler;

    private Vector3 currentMovement;

    private Rigidbody rb;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = InputHandler.Instance;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        print(currentMovement * Time.deltaTime);
    }

    void HandleMovement()
    {
        float speed = shipSpeed * (inputHandler.SprintValue > 0 ? boostMultiplier : 1f);

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        // currentMovement.y = 0f;
        currentMovement.z = worldDirection.z * speed;

        // characterController.Move(currentMovement * Time.deltaTime);
        Vector3 forceDirection = transform.forward * inputHandler.MoveInput.y * shipSpeed * boostMultiplier;
        rb.AddForce(forceDirection, ForceMode.Acceleration);
    }
}
