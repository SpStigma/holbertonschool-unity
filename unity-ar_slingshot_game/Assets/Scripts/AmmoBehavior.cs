using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    public GameObject ammo;   // Ammo prefab or GameObject
    public float force = 5f;  // Maximum force applied
    public float verticalForceMultiplier = 2f; // Multiplier for vertical force
    private bool isDragging = false;   // Tracks if the ammo is being dragged
    private Rigidbody rb;
    private Vector3 initialPosition;   // Starting position before the drag
    private float dragDistance;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        initialPosition = ammo.transform.position;   // Save the initial position
    }

    public void Update()
    {
        // Begin drag when the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // Handle dragging behavior
        if (isDragging)
        {
            DragAmmo();
        }

        // Launch the ammo when the mouse button is released
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            dragDistance = initialPosition.y - ammo.transform.position.y;

            if (dragDistance > 0)
            {
                ammo.transform.SetParent(null);
                
                // Center the ammo at the launch point
                ammo.transform.position = new Vector3(ammo.transform.position.x, ammo.transform.position.y, ammo.transform.position.z);

                // Calculate the force to apply, clamped to a maximum value
                float appliedForce = Mathf.Clamp(dragDistance * force, 0, force);
                
                rb.isKinematic = false;
                
                // Add horizontal force based on camera's forward direction
                Vector3 launchDirection = Camera.main.transform.forward;

                // Add a vertical component to create a curve
                Vector3 forceToApply = launchDirection * appliedForce + Vector3.up * (appliedForce * verticalForceMultiplier);
                
                rb.AddForce(forceToApply, ForceMode.Impulse);
            }
            else
            {
                ammo.transform.position = initialPosition;
            }
        }
        
        DeleteAmmo();
    }

    private void DragAmmo()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(ammo.transform.position).z;
        ammo.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void DeleteAmmo()
    {
        if (ammo.transform.position.y < -10)
        {
            Destroy(ammo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            DeleteAmmo();
        }
    }
}
