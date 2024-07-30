using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle isYInverted;
    public Slider BGMSlider;
    public AudioMixer master;

    public void Start()
    {
        float currentVolume;
        if (master.GetFloat("BGM", out currentVolume))
        {
            BGMSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            BGMSlider.value = 1f;
        }
    }
    public void Back()
    {
        SceneController.instance.LoadPreviousScene();
    }

    public void Apply()
    {
        if(isYInverted.isOn)
        {
            GameSettings.isInverted = true;
            SceneController.instance.LoadPreviousScene();
        }
        else
        {
            GameSettings.isInverted = false;
            SceneController.instance.LoadPreviousScene();
        }
    }

    public void SetSoundBGM(float slider)
    {
        if (slider <= 0.001f)
        {
            master.SetFloat("BGM", -80f);
        }
        else
        {
            master.SetFloat("BGM", Mathf.Log10(slider) * 20);
        }
    }
}
