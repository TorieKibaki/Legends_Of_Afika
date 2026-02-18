using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float menuVolume = 0.8f;
    [Range(0f, 1f)] public float gameplayVolume = 0.3f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        // Singleton pattern specifically for continuous music
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If a new AudioManager tries to load (e.g. from a new scene), 
            // destroy it so the original one keeps playing.
            Destroy(gameObject);
            return;
        }

        // Apply initial volume based on current scene
        ApplyVolumeForScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        ApplyVolumeForScene(scene);
    }

    public void SetMenuVolume(float volume)
    {
        menuVolume = volume;
        // Apply immediately if in menu
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu" && musicSource != null)
        {
            musicSource.volume = menuVolume;
        }
    }

    public void SetGameplayVolume(float volume)
    {
        gameplayVolume = volume;
        // Apply immediately if in game
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" && musicSource != null)
        {
            musicSource.volume = gameplayVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
    }

    private void ApplyVolumeForScene(UnityEngine.SceneManagement.Scene scene)
    {
        if (musicSource == null) return;

        if (scene.name == "MainMenu")
        {
            musicSource.volume = menuVolume;
        }
        else
        {
            // Assume any other scene is a level
            musicSource.volume = gameplayVolume;
        }

        if (sfxSource != null) sfxSource.volume = sfxVolume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null) return;

        // If the same music is already playing, don't restart it
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null) musicSource.Stop();
    }
}