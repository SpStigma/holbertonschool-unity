using UnityEngine;

public class ManagerTarget : MonoBehaviour
{
    public GameObject playAgainButton;

    public void Update()
    {
        ShowButtonPlayAgain();
    }

    public void ShowButtonPlayAgain()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Target");

        if(target.Length == 0)
        {
            playAgainButton.SetActive(true);
        }
        else
        {
            playAgainButton.SetActive(false);
        }
    }
}
