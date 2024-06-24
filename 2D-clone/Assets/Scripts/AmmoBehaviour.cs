using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }

    void Shoot()
    {
        rb.velocity = transform.up * speed;
    }

    public void Update()
    {
        DestructOutsideCam();
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
}
