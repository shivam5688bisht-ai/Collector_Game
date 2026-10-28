using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f; // Standard Earth gravity

    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector3 verticalVelocity;

    [Header("Camera & Rotation")]
    public Transform mainCamera; 
    public float mouseSensitivity = 2f;
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (mainCamera == null && Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }

        // Locks the mouse cursor inside the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Automatically called by the New Input System (Behavior: Send Messages)
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    // Automatically called by the New Input System (Behavior: Send Messages)
    public void OnLook(InputValue value)
    {
        Vector2 lookInput = value.Get<Vector2>();

        // Turn the Player horizontally (Left/Right)
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity * 0.1f);

        // Tilt the Camera vertically (Up/Down)
        if (mainCamera != null)
        {
            cameraPitch -= lookInput.y * mouseSensitivity * 0.1f;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f); 
            mainCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }

    void Update()
    {
        // 1. Calculate direction based on where the player faces
        Vector3 moveDirection = (transform.forward * inputDirection.y + transform.right * inputDirection.x).normalized;
        
        // Move the player horizontally
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 2. Handle gravity checking if we are touching the ground
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            // Small downward force to keep the character securely snapped to slopes/ground
            verticalVelocity.y = -2f; 
        }
        else
        {
            // Apply regular gravity accumulation while airborne
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        // Apply the downward gravity movement
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
