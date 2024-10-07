using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    public GameObject ammoPrefab;
    public Transform cameraTransform;
    public int maxAmmoCount = 5; // Maximum number of ammo allowed
    private int currentAmmoCount = 0; // Tracks the number of ammo spawned

    void Update()
    {
        SpawnAmmoAtCamera();
    }

    public void SpawnAmmoAtCamera()
    {
        // Check if there is no active ammo and if the limit of ammo hasn't been reached
        GameObject currentAmmo = GameObject.FindGameObjectWithTag("Ammo");

        if (currentAmmo == null && currentAmmoCount < maxAmmoCount)
        {
            // Instantiate the ammo at the camera's position
            GameObject ammoInstance = Instantiate(ammoPrefab, cameraTransform.position, cameraTransform.rotation);

            // Make the ammo a child of the camera
            ammoInstance.transform.SetParent(cameraTransform);

            // Position the ammo slightly in front of the camera
            ammoInstance.transform.localPosition = new Vector3(0, 0, 0.5f); // Adjust the local position as needed

            // Increment the count of ammo spawned
            currentAmmoCount++;
        }
    }
}
