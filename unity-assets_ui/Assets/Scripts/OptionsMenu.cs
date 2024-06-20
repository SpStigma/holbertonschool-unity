using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Button backButton;

    // Variable to store the name of the previous scene
    private string previousScene;

    void Start()
    {
        // Get the previous scene from PlayerPrefs
        previousScene = PlayerPrefs.GetString("PreviousScene", "MainMenu"); // Default to "MainMenu" if not set

        // Add listener to the back button
        backButton.onClick.AddListener(Back);
    }

    public void Back()
    {
        SceneController.instance.LoadPreviousScene();
    }
}
