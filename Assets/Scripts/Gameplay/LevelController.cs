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

   
    public void OnSafeCollected()
    {
        if (UIManager.instance != null)
        {
           
            UIManager.instance.ShowFact("You found an Ancient Artefact!");
        }
    }

    
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