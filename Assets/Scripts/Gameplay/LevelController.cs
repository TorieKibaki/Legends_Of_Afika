using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // 1. Called by the Door
    public void OnDoorPassed()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowEnd("Level Complete!");
        }
        else
        {
            LoadNextLevel();
        }
    }

    // 2. Called by SafeCollectible.cs (This fixes your current error!)
    public void OnSafeCollected()
    {
        if (UIManager.instance != null)
        {
            // This triggers the Fact Panel in your UIManager
            UIManager.instance.ShowFact("You found an Ancient Artefact!");
        }
    }

    // 3. Called by UIManager after the "End Panel" timer
    public void OnEndPanelClosed()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Returning to Main Menu.");
            SceneManager.LoadScene("MainMenu");
        }
    }
}