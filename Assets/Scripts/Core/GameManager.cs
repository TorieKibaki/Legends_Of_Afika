using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool hasSafeCollectible;

    private List<DisappearingTile> tiles = new List<DisappearingTile>();
    // NEW: List to track collectibles
    private List<SafeCollectible> collectibles = new List<SafeCollectible>();

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
        // IMPORTANT: Clear lists on new scene load so we don't reference deleted objects
        tiles.Clear();
        collectibles.Clear();
    }

    public void CollectSafe()
    {
        Debug.Log("Safe Collectible Collected! hasSafeCollectible set to TRUE.");
        hasSafeCollectible = true;
    }

    // --- TILE MANAGEMENT ---
    public void RegisterTile(DisappearingTile tile)
    {
        if (!tiles.Contains(tile))
            tiles.Add(tile);
    }

    // --- COLLECTIBLE MANAGEMENT ---
    // NEW: Register method for collectibles
    public void RegisterCollectible(SafeCollectible collectible)
    {
        if (!collectibles.Contains(collectible))
            collectibles.Add(collectible);
    }

    // --- GLOBAL RESET ---
    // Update your respawn logic to call this method
    public void ResetLevelState()
    {
        // Reset the bool
        hasSafeCollectible = false;

        // Reset all tiles
        foreach (DisappearingTile tile in tiles)
        {
            if (tile != null) tile.ResetTile();
        }

        // NEW: Reset all collectibles
        foreach (SafeCollectible item in collectibles)
        {
            if (item != null) item.ResetCollectible();
        }
    }

    // This remains for backward compatibility with your existing PlayerRespawn script
    public void ResetTiles()
    {
        ResetLevelState();
    }

    // --- SAVE SYSTEM ---
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