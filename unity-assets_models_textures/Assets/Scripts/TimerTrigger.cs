using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    // Reference to the script we want to activate.
    public Timer timerscript;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerscript.enabled = true;
        }
    }
}
