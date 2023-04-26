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
        audioMixer.SetFloat(parameterName, Mathf.Log10(slider.value) * 20);
    }

    public void OnSliderValueChanged()
    {
        audioMixer.SetFloat(parameterName, Mathf.Log10(slider.value) * 20);
    }
}
