using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    void Start()
    {
        button1.onClick.AddListener(() => LevelSelect(2));
        button2.onClick.AddListener(() => LevelSelect(3));
        button3.onClick.AddListener(() => LevelSelect(4));
    }
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void Options()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
