using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.18f;
    public float jumpHeight = 8f; 

    // Vector to store the player's velocity
    Vector3 velocity;

    // Ground check variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    private Vector3 resetPosition = new Vector3(0, 20, 0);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded using a sphere at the groundCheck position
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If the player is grounded and falling, set the y velocity to a small negative value
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get the input from the horizontal and vertical axes (WASD or arrow keys)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement vector based on input
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = cameraForward * z + Camera.main.transform.right * x;

        // Move the player based on the movement vector and speed
        controller.Move(movement * speed * Time.deltaTime);

        // If the jump button is pressed and the player is grounded, calculate the jump velocity
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity to the y velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the player based on the velocity vector multiplied by 2 time Time.
        controller.Move(velocity * Time.deltaTime);

        ResetFalling();
    }

    public void ResetFalling()
    {
        if (transform.position.y <  -20)
        {
            transform.position = resetPosition;
        }
    }
}
