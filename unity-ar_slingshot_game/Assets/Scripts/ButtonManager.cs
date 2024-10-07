using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ButtonManager : MonoBehaviour
{
    public GameObject AmmoManager;

    public void RestartButton()
    {
        ChosePlanes.selectedPlane = null;
        ChosePlanes.Instance.EnablePlanes();
        SpawnAmmo.Instance.currentAmmoCount = 0;
        AmmoManager.SetActive(false);
        ScoreManager.Instance.scoreTotal = 0;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
