using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool hasSafeCollectible;
    private List<DisappearingTile> tiles = new List<DisappearingTile>();

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

    // REGISTER TILE
    public void RegisterTile(DisappearingTile tile)
    {
        if (!tiles.Contains(tile))
            tiles.Add(tile);
    }

    // RESET ALL TILES
    public void ResetTiles()
    {
        foreach (DisappearingTile tile in tiles)
        {
            tile.ResetTile();
        }
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
