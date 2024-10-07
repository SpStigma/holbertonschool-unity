using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class TargetBehaviour : MonoBehaviour
{
    public static TargetBehaviour Instance;
    private GameObject capsule;
    public float movementSpeed = 1f;
    public float moveDuration = 2f; // Duration for the movement
    public float boundaryThreshold = 0.1f; // Distance to consider the capsule as "reached" its target
    public float pauseDuration = 0.5f; // Time to pause after reaching a boundary point
    public float minScale = 0.1f; // Minimum scale factor
    public float maxScale = 1f; // Maximum scale factor
    public float scaleDistance = 5f; // Distance at which the scale reaches its minimum
    public int scoreTarget = 10;

    private bool isMoving = false; // Track if the capsule is currently moving

    void Start()
    {
        Instance = this;
        // Assign the capsule to the GameObject this script is attached to
        capsule = this.gameObject;
    }

    void Update()
    {
        if (!isMoving && ChosePlanes.selectedPlane != null)
        {
            // Get the boundary points in the selected plane's local space
            NativeArray<Vector2> boundaryPoints = ChosePlanes.selectedPlane.boundary;

            if (boundaryPoints.Length > 0)
            {
                // Convert the boundary points from local space to world space
                Vector3[] worldBoundaryPoints = new Vector3[boundaryPoints.Length];

                for (int i = 0; i < boundaryPoints.Length; i++)
                {
                    // Convert the 2D boundary points to 3D, keeping y = 0 (fixed to plane level)
                    Vector3 localBoundaryPoint = new Vector3(boundaryPoints[i].x, 0, boundaryPoints[i].y);
                    worldBoundaryPoints[i] = ChosePlanes.selectedPlane.transform.TransformPoint(localBoundaryPoint);
                }

                // Start the coroutine to move the capsule within bounds
                StartCoroutine(MoveCapsuleWithinBounds(worldBoundaryPoints));
            }
        }

        // Scale the capsule based on its distance from the camera
        ScaleCapsule();
    }

    // Coroutine to move the capsule smoothly to a new position
    IEnumerator MoveCapsuleWithinBounds(Vector3[] worldBoundaryPoints)
    {
        isMoving = true;

        // Get the current Y position to maintain it
        float originalYPosition = capsule.transform.position.y;

        Vector3 randomPointInBounds = GetRandomPointInPolygon(worldBoundaryPoints);
        Vector3 startPosition = capsule.transform.position;

        // Ensure the random point keeps the original Y position
        randomPointInBounds.y = originalYPosition;

        float elapsedTime = 0f;

        // Move the capsule smoothly over the specified duration
        while (elapsedTime < moveDuration)
        {
            // Interpolate position, keeping the Y axis constant
            Vector3 newPosition = Vector3.Lerp(startPosition, randomPointInBounds, elapsedTime / moveDuration);
            newPosition.y = ChosePlanes.selectedPlane.transform.position.y; // Keep Y position on the plane
            capsule.transform.position = newPosition;

            elapsedTime += Time.deltaTime; // Increment the time elapsed
            yield return null; // Wait for the next frame

            // If the capsule is close enough to the target point, break the loop
            if (Vector3.Distance(capsule.transform.position, randomPointInBounds) < boundaryThreshold)
            {
                break;
            }
        }

        // Ensure the capsule reaches the exact target position and keeps Y fixed
        randomPointInBounds.y = ChosePlanes.selectedPlane.transform.position.y; // Ensure it stays on the plane
        capsule.transform.position = randomPointInBounds;

        // Wait for a short period before moving again
        yield return new WaitForSeconds(pauseDuration);

        isMoving = false; // Allow movement to start again
    }

    void ScaleCapsule()
    {
        // Get the camera's position
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Calculate the distance from the capsule to the camera
            float distance = Vector3.Distance(capsule.transform.position, mainCamera.transform.position);

            // Calculate the scale based on the distance
            float scale = Mathf.Lerp(maxScale, minScale, distance / scaleDistance);
            scale = Mathf.Clamp(scale, minScale, maxScale); // Ensure the scale stays within bounds

            // Apply the new scale to the capsule
            capsule.transform.localScale = new Vector3(scale, scale, scale);

            // Get the Y position of the plane
            float planeYPosition = ChosePlanes.selectedPlane.transform.position.y;

            // Adjust the capsule's Y position based on the plane's Y position and the capsule's height
            float halfHeight = scale / 2; // Calculate half of the current scale
            capsule.transform.position = new Vector3(capsule.transform.position.x, planeYPosition + halfHeight, capsule.transform.position.z);
        }
    }

    // Function to get a random point on the plane's boundary (simplified version)
    Vector3 GetRandomPointInPolygon(Vector3[] boundary)
    {
        // Pick a random point from the boundary points
        int randomIndex = Random.Range(0, boundary.Length);
        return boundary[randomIndex];
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ammo"))
        {
            ScoreManager.Instance.scoreTotal += scoreTarget;
            Destroy(gameObject);
        }
    }

    public void DestroyCapsule()
    {
        // Find all game objects with the tag "Target"
        GameObject[] capsules = GameObject.FindGameObjectsWithTag("Target");
        
        // Iterate through each found capsule and destroy it
        foreach (GameObject caps in capsules)
        {
            Destroy(caps);
        }
    }
}
