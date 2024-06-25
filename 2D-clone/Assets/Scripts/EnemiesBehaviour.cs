using UnityEngine;

public class EnemiesBehaviour : MonoBehaviour
{
    public static EnemiesBehaviour instance;
    public float speed = 5f;
    public float minDelay = 1f;
    public float maxDelay = 3f;
    public float smoothTime = 0.5f;
    public float rotationSpeed = 2f;

    public Transform player;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float moveDelay;
    public float health = 100;

    void Start()
    {
        instance = this;
        SetRandomTargetPosition();
        SetRandomDelay();
    }

    void Update()
    {
        MoveTowardsTarget();
        SmoothLookAtPlayer();
        DestroyEnnemy();
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
            SetRandomDelay();
        }
    }

    void SetRandomTargetPosition()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);

        targetPosition = new Vector3(randomX, randomY, 0);
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
    }

    void SetRandomDelay()
    {
        moveDelay = Random.Range(minDelay, maxDelay);
        Invoke(nameof(SetRandomTargetPosition), moveDelay);
    }

    void SmoothLookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust angle if necessary
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void DestroyEnnemy()
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
