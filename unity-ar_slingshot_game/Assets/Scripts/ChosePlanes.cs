using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using Unity.Mathematics;

public class ChosePlanes : MonoBehaviour
{
    public static ChosePlanes Instance;
    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public static ARPlane selectedPlane;
    public GameObject button;
    public GameObject Target;

    public void Start()
    {
        Instance = this;
    }
    void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds))
                {
                    // Get the selected plane
                    ARPlane plane = hits[0].trackable as ARPlane;
                    if (plane != null)
                    {
                        SelectPlane(plane);
                    }
                }
            }
        }
        */

        if (selectedPlane != null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))  // Mouse left click simulates touch
        {
            Vector2 screenPosition = Input.mousePosition;

            // Raycast against detected planes
            if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinBounds))
            {
                ARPlane hitPlane = hits[0].trackable as ARPlane;

                // Select the plane if it's valid
                if (hitPlane != null)
                {
                    Debug.Log("plane selected");
                    SelectPlane(hitPlane);
                }
            }
        }
    }

    public void SelectPlane(ARPlane plane)
    {
        selectedPlane = plane;
        DisablePlanes();
        button.SetActive(true);

    }

    public void DisablePlanes()
    {
        ARPlaneManager planeManager = GetComponent<ARPlaneManager>();

        foreach (var plane in planeManager.trackables)
        {
            // Ensure the plane hasn't been destroyed or disabled
            if (plane != ChosePlanes.selectedPlane)
            {
                // Disable the plane's visual representation if it exists
                var planeVisualiser = plane.GetComponent<ARPlaneMeshVisualizer>();
                if(planeVisualiser != null)
                {
                    planeVisualiser.enabled = false;
                }
            }
        }
    }

    public void EnablePlanes()
    {
        ARPlaneManager planeManager = GetComponent<ARPlaneManager>();

        foreach (var plane in planeManager.trackables)
        {
            var planeVisualiser = plane.GetComponent<ARPlaneMeshVisualizer>();
            if(planeVisualiser != null)
            {
                planeVisualiser.enabled = true;
            }
        }
    }

    public void SpawnTarget()
    {
        float n = 5;
        if(selectedPlane != null)
        {
            for(int i = 0; i < n; i++)
            {
                Instantiate(Target);
                button.gameObject.SetActive(false);
            }
        }
    }
}
