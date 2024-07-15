using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f; // Use -9.81 for realistic gravity
    public float jumpHeight = 1.5f; // Adjust for realistic jump height

    private Animator animator;
    private Transform modelTransform;

    // Vector to store the player's velocity
    private Vector3 velocity;

    // Ground check variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Vector3 resetPosition = new Vector3(0, 20, 0);

    void Start()
    {
        // Get necessary components at start
        controller = GetComponent<CharacterController>();
        modelTransform = transform.Find("ty"); // Make sure "ty" is the correct model name
        animator = modelTransform.GetComponent<Animator>();
    }

    void Update()
    {
        // Check if player is grounded using a sphere at groundCheck position
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Apply gravity if player is grounded and falling
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("isJumping", false); // Reset jump state when grounded
            animator.SetBool("isFalling", false);
        }

        // Get horizontal and vertical movement input (WASD or arrow keys)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement vector based on input
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = cameraForward * z + Camera.main.transform.right * x;

        // Move the player based on movement vector and speed
        controller.Move(movement * speed * Time.deltaTime);

        // Rotate player to face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        // Update animator's isRunning parameter based on movement
        bool isRunning = movement.magnitude > 0;
        animator.SetBool("isRunning", isRunning);

        // Handle jump if jump button is pressed and player is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("isJumping", true);
        }

        // Apply gravity to y velocity
        velocity.y += gravity * Time.deltaTime;

        // Move player based on velocity vector multiplied by delta time
        controller.Move(velocity * Time.deltaTime);

        // Reset position if player falls below a certain threshold
        ResetFalling();
    }

    // Reset player falling position
    public void ResetFalling()
    {
        if (transform.position.y < -20)
        {
            animator.SetBool("isFalling", true);
            transform.position = resetPosition;
        }
    }
    void LateUpdate()
    {
        // Sync model position with CharacterController position
        Vector3 modelPosition = modelTransform.position;
        modelPosition.y = transform.position.y - 1f; // Maintain same Y position as CharacterController
        modelPosition.x = transform.position.x; // Maintain same X position as CharacterController
        modelPosition.z = transform.position.z; // Maintain same Z position as CharacterController
        modelTransform.position = modelPosition;

        // Sync model rotation with CharacterController rotation
        modelTransform.rotation = transform.rotation;
    }
}
