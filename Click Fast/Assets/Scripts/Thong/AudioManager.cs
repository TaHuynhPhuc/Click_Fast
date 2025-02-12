using UnityEngine;

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

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlaySoundEffect(string soundType)
    {
        switch (soundType)
        {
            case "correct":
                sfxSource.PlayOneShot(correctSound);
                break;
            case "wrong":
                sfxSource.PlayOneShot(wrongSound);
                break;
            case "click":
                sfxSource.PlayOneShot(buttonClickSound);
                break;
            default:
                Debug.LogWarning("Âm thanh không hợp lệ: " + soundType);
                break;
        }
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }
}