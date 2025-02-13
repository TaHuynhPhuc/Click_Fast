using UnityEngine;
using UnityEngine.UI; // Import UI để dùng Slider

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip buttonClickSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.volume = musicVolume; // Áp dụng volume
            musicSource.Play();
        }
    }

    public void PlaySoundEffect(string soundType)
    {
        switch (soundType)
        {
            case "correct":
                sfxSource.PlayOneShot(correctSound, sfxVolume);
                break;
            case "wrong":
                sfxSource.PlayOneShot(wrongSound, sfxVolume);
                break;
            case "click":
                sfxSource.PlayOneShot(buttonClickSound, sfxVolume);
                break;
            default:
                Debug.LogWarning("Âm thanh không hợp lệ: " + soundType);
                break;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }
}
