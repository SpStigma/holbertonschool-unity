using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle isYInverted;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMixer master;

    public void Start()
    {
        float currentVolume;
        float currentVolumeSFX;
        if (master.GetFloat("BGM", out currentVolume))
        {
            BGMSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            BGMSlider.value = 1f;
        }

        if (master.GetFloat("SFX", out currentVolumeSFX))
        {
            SFXSlider.value = Mathf.Pow(10, currentVolumeSFX / 20);
        }
        else
        {
            SFXSlider.value = 1f;
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

    public void SetSoundSFX(float slider)
    {
        if (slider <= 0.001f)
        {
            master.SetFloat("SFX", -80f);
        }
        else
        {
            master.SetFloat("SFX", Mathf.Log10(slider) * 20);
        }
    }
}
