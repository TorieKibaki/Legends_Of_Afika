using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject roadmapPanel; // The Level Select Screen

    void Start()
    {
        ShowMain();
    }

    public void ShowMain()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        roadmapPanel.SetActive(false);
    }

    public void OnPlayClicked()
    {
        // Go to Roadmap (Level Select)
        mainPanel.SetActive(false);
        roadmapPanel.SetActive(true);
    }

    public void OnSettingsClicked()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnBackClicked()
    {
        ShowMain();
    }

    // --- ROADMAP LEVEL SELECTION ---
    public void LoadLevel(int levelIndex)
    {
        string levelName = "Level" + levelIndex; // Assumes scenes are named Level1, Level2...
        
        if (SceneLoader.instance != null)
            SceneLoader.instance.LoadScene(levelName);
        else
            SceneManager.LoadScene(levelName);
    }
}
