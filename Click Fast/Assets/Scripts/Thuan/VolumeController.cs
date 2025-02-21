using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private AudioManager audioManager;

    private void Start()
    {
        // Kiểm tra nếu AudioManager chưa có, thì tìm nó trong Scene
        if (AudioManager.Instance == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        else
        {
            audioManager = AudioManager.Instance;
        }

        // Load volume từ PlayerPrefs
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 1f);

        // Gán sự kiện thay đổi giá trị của slider
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Áp dụng giá trị volume ngay từ đầu
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    private void Update()
    {
        // Nếu AudioManager bị mất, tìm lại trong Scene
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.musicSource.volume = volume;
            PlayerPrefs.SetFloat("musicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.sfxSource.volume = volume;
            PlayerPrefs.SetFloat("sfxVolume", volume);
        }
    }
}
