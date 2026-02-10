using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    public AudioSource musicSource;
    public AudioSource sfxSource;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}