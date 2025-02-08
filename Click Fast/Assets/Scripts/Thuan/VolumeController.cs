using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float saveVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = saveVolume;
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(saveVolume) * 20);
        }
        else
        {
            volumeSlider.value = 0f;
            SetVolume(0f);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
