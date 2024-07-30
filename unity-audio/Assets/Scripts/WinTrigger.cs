using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TextMeshProUGUI timerText;
    public GameObject winCanvas;
    public AudioSource BGM;
    public AudioSource sting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerScript.enabled = false;
            winCanvas.SetActive(true);
            Timer.instance.Win();
            Timer.instance.timerText.enabled = false;
            BGM.Stop();
            sting.Play();
        }
    }
}
