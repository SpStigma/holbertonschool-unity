using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public Timer timerScript;
    public TextMeshProUGUI timerText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerScript.enabled = false;

            timerText.fontSize = 60;
            timerText.color = Color.green;
        }
    }
}
