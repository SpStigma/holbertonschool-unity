using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private float minX, maxX, minY, maxY;
    private Camera cam;
    public GameObject ammo;
    public float cooldownAmmo = 2;
    public float time;

    void Start()
    {
        // Get the main Camera
        cam = Camera.main;

        // Calculate the boundaries of the main cam
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;

        Movement();

        if (time >= cooldownAmmo)
        {
            Projectile();
            time = 0f;
        }
    }

    public void Movement()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // Limit the The player movement in the boundaries of the cam
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;

        // the player look at the mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));

        Vector2 direction = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Adjust the angle if necessary
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void Projectile()
    {
        Instantiate(ammo, transform.position, transform.rotation);
    }
}
