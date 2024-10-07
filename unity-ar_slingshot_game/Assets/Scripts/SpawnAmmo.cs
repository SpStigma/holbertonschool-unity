using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    public static SpawnAmmo Instance;
    public GameObject ammoPrefab;
    public Transform cameraTransform;
    public int maxAmmoCount = 8;
    public int AmmoLeft = 8; // Maximum number of ammo allowed
    public int currentAmmoCount = 0; // Tracks the number of ammo spawned
    public GameObject buttonRestart;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        // Only check for ammo if the current ammo count is less than the max
        if (currentAmmoCount < maxAmmoCount)
        {
            SpawnAmmoAtCamera();
        }

        // Check if there are no active ammo and the current count has reached max
        if (currentAmmoCount >= maxAmmoCount && GameObject.FindGameObjectWithTag("Ammo") == null)
        {
            TargetBehaviour.Instance.DestroyCapsule();
            buttonRestart.SetActive(true);
        }
    }

    public void SpawnAmmoAtCamera()
    {
        // Check if there is no active ammo
        GameObject currentAmmo = GameObject.FindGameObjectWithTag("Ammo");

        if (currentAmmo == null)
        {
            // Instantiate the ammo at the camera's position
            GameObject ammoInstance = Instantiate(ammoPrefab, cameraTransform.position, cameraTransform.rotation);

            // Make the ammo a child of the camera
            ammoInstance.transform.SetParent(cameraTransform);

            // Position the ammo slightly in front of the camera
            ammoInstance.transform.localPosition = new Vector3(0, 0, 0.5f); // Adjust the local position as needed

            // Increment the count of ammo spawned
            currentAmmoCount++;
            AmmoLeft--;
        }
    }
}
