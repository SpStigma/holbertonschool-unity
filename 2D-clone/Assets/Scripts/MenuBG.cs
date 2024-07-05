using UnityEngine;
using TMPro;

public static class GlobalVariables
{
    public static float score;
}

public class MenuBG : MonoBehaviour
{
    public static MenuBG instance;
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        instance = this;
        SetScore();
    }

    public void Update()
    {
        SetScore();
    }

    public void SetScore()
    {
        scoreText.text = "Score: " + GlobalVariables.score.ToString("00");
    }

    public void IncreaseScore(int amount)
    {
        GlobalVariables.score += amount;
    }
}
