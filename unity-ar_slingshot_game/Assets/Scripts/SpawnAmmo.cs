using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    public GameObject ammoPrefab;
    public Transform cameraTransform;

    public void SpawnAmmoAtCamera()
    {
        // Instancie l'ammo à la position de la caméra
        GameObject ammoInstance = Instantiate(ammoPrefab, cameraTransform.position, cameraTransform.rotation);

        // Rend l'ammo enfant de la caméra
        ammoInstance.transform.SetParent(cameraTransform);

        // Si tu veux positionner l'ammo légèrement en avant de la caméra
        ammoInstance.transform.localPosition = new Vector3(0, 0, 0.5f); // Ajuste la position locale selon tes besoins
    }

}
