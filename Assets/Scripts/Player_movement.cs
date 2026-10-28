using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    
    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Gather input from WASD / Arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 2. Calculate horizontal direction based on player facing direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        
        // 3. Move the controller laterally
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 4. Apply manual gravity calculations
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps the player snapped to the floor
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
