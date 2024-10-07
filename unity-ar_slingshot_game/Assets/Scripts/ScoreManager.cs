using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI score;
    public int scoreTotal = 0;
    public TextMeshProUGUI ammoLeft;

    void Update()
    {
        Instance = this;
        Score();
        DisplayAmmo();
    }

    private void Score()
    {
        score.text = scoreTotal.ToString();
    }

    private void DisplayAmmo()
    {
        if(SpawnAmmo.Instance != null)
        {
            if(SpawnAmmo.Instance.AmmoLeft == 0)
            {
                ammoLeft.text = "Last One";
            }
            else
            {
                ammoLeft.text = "Ammo : " + SpawnAmmo.Instance.AmmoLeft.ToString();
            }
        }
        else
        {
            ammoLeft.text = "Ammo : 0";
        }
    }
}
