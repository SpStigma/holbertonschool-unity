using UnityEngine;

public class AmmoEnnemies : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    public int damageAmount = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }

    void Shoot()
    {
        rb.velocity = transform.up * speed;
    }

    public void DestructOutsideCam()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Check if the ammo is outside the camera's view
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.TakeDamageOnPlayer(damageAmount);
            Destroy(gameObject);
        }
    }
}
