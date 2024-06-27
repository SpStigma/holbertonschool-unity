using UnityEngine;

public class EnemiesBehaviour : MonoBehaviour
{
    public static EnemiesBehaviour instance;
    public float health;
    private Camera cam;
    private float minX, maxX, minY, maxY;
    public float speed = 5;
    private Vector3 targetPoint; // Store current target point for movement
    private Transform playerPosition; // Changed to Transform for storing player's transform

    public float rotationSpeed = 5f; // Rotation speed for enemy to face player

    public void Start()
    {
        instance = this;
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerPosition = playerMovement.transform;
        }
        cam = Camera.main;
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;

        SelectTargetPointInCam(); // Start by selecting a target point
    }

    public void Update()
    {
        // Move towards the selected target point
        MoveTowards(targetPoint, speed);

        // Look at the player if playerPosition is valid
        if (playerPosition != null)
        {
            LookAtPlayer();
        }

        // Check if arrived at the target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            SelectTargetPointInCam(); // Select a new target point when arrived
        }
    }

    public void SelectTargetPointInCam()
    {
        float targetX = Random.Range(minX, maxX);
        float targetY = Random.Range(minY, maxY);

        targetPoint = new Vector3(targetX, targetY, 0);
    }

    public void MoveTowards(Vector3 targetPoint, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
    }

    public void LookAtPlayer()
    {
        Vector3 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust angle if necessary
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void DestroyEnemy()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
