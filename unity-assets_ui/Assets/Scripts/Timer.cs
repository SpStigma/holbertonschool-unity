using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI timerText;
    private float time;
    public TextMeshProUGUI timerToGive;

    void Start()
    {
        instance = this;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            time += Time.deltaTime;
            SetTimer();
        }
    }

    public void SetTimer()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100f);

        timerText.text = $"{minutes}:{seconds:00}.{milliseconds:00}";
        
    }

    public void Win()
    {
        timerToGive.text = timerText.text;
    }
}
