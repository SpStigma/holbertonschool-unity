using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public PlayerController player;
    public Canvas Timer;


   public void OnCutsceneEnd()
   {
        mainCamera.SetActive(true);
        player.enabled = true;
        Timer.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
   }
}
