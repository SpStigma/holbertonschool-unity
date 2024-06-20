using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle applyButton;

    public void Back()
    {
        SceneController.instance.LoadPreviousScene();
    }

    public void Apply()
    {
    }
}
