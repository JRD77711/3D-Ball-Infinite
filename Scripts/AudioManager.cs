using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip coinClip;
    public AudioClip destroyClip;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(inGameMusic);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if(musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if(sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
