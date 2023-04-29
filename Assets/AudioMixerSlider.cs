using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider.value == 0f)
        {
            slider.value = 0.001f; // Set a small non-zero value to avoid issues with log10
        }
        audioMixer.SetFloat(parameterName, Mathf.Log10(slider.value) * 20);

        // Load slider value from PlayerPrefs
        if (PlayerPrefs.HasKey(parameterName))
        {
            slider.value = PlayerPrefs.GetFloat(parameterName);
        }

        float currentValue;
        audioMixer.GetFloat(parameterName, out currentValue);
        slider.value = Mathf.Pow(10, currentValue / 20);
    }

    public void OnSliderValueChanged()
    {
        if (slider.value == 0f)
        {
            slider.value = 0.001f; // Set a small non-zero value to avoid issues with log10
        }
        audioMixer.SetFloat(parameterName, Mathf.Log10(slider.value) * 20);

        // Save slider value to PlayerPrefs
        PlayerPrefs.SetFloat(parameterName, slider.value);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        // Save slider value to PlayerPrefs when the scene changes
        PlayerPrefs.SetFloat(parameterName, slider.value);
        PlayerPrefs.Save();
    }
}
