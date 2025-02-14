using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton để gọi từ bất cứ đâu

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip clickSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public AudioSource musicSource;
    public AudioSource sfxSource;

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
            musicSource.volume = 1f; 
            musicSource.Play();
        }
    }

    public void PlaySoundEffect(string soundType)
    {
        switch (soundType)
        {
            case "click":
                sfxSource.PlayOneShot(clickSound);
                break;
            case "correct":
                sfxSource.PlayOneShot(correctSound);
                break;
            case "wrong":
                sfxSource.PlayOneShot(wrongSound);
                break;
            default:
                Debug.LogWarning("Không tìm thấy âm thanh: " + soundType);
                break;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }
}
