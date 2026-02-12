using UnityEngine;
using UnityEngine.SceneManagement; // Essential for scene switching

public class LevelController : MonoBehaviour
{
    // This is the function your Door script calls
    public void OnDoorPassed()
    {
        Debug.Log("Level Complete! Moving to next level...");
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        // 1. Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 2. Calculate the next index
        int nextSceneIndex = currentSceneIndex + 1;

        // 3. Check if the next index exists in your Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("You've reached the last level! Returning to Main Menu.");
            SceneManager.LoadScene(0); // Optional: Load index 0 (usually the Menu)
        }
    }
}