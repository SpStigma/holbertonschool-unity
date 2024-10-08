using UnityEditor.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject AmmoManager;

    public void RestartButton()
    {
        TargetBehaviour.Instance.DestroyCapsule();
        ChosePlanes.selectedPlane = null;
        ChosePlanes.Instance.EnablePlanes();
        SpawnAmmo.Instance.currentAmmoCount = 0;
        AmmoManager.SetActive(false);
        ScoreManager.Instance.scoreTotal = 0;
        SpawnAmmo.Instance.AmmoLeft = SpawnAmmo.Instance.maxAmmoCount;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgainButton()
    {
        SpawnAmmo.Instance.currentAmmoCount = 0;
        ScoreManager.Instance.scoreTotal = 0;
        SpawnAmmo.Instance.AmmoLeft = SpawnAmmo.Instance.maxAmmoCount;
        
    }
}
