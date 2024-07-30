using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        isPaused = true;
        paused.TransitionTo(.01f);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        unPaused.TransitionTo(.01f);
    }

    public void Restart()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        string nameScene = activeScene.name;

        SceneManager.LoadScene(nameScene);

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
        Time.timeScale = 1;
    }
}
