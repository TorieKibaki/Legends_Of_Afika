using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool hasSafeCollectible;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasSafeCollectible = false;
    }

    public void CollectSafe()
    {
        hasSafeCollectible = true;
    }

    // SAVE LEVEL UNLOCK
    public void UnlockLevel(int levelNumber)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (levelNumber > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelNumber);
            PlayerPrefs.Save();
        }
    }

    public int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 1);
    }
}
