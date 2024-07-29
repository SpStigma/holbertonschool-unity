using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public int oldScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Options")
        {
            oldScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(oldScene);
    }
}
