using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject roadmapPanel; 

    [Header("Level Selection")]
    public Button[] levelButtons; 

    [Header("Settings UI")]
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    void Start()
    {
        // Ensure buttons are updated at start
        UpdateLevelButtons();

        // Initialize Sliders
        if (AudioManager.instance != null)
        {
            if (musicVolumeSlider != null) 
            {
                // Init slider based on current music volume
                musicVolumeSlider.value = AudioManager.instance.menuVolume;
                musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = AudioManager.instance.sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            }
        }

        // Support returning to roadmap directly
        if (GameManager.instance != null && GameManager.instance.returnToRoadmap)
        {
            ShowRoadmap();
            GameManager.instance.returnToRoadmap = false; // Reset flag
        }
        else
        {
            ShowMain();
        }
    }

    void OnEnable()
    {
        // Update whenever the script/object is enabled (e.g. switching panels)
        UpdateLevelButtons();
    }
    
    public void ShowRoadmap()
    {
        mainPanel.SetActive(false);
        roadmapPanel.SetActive(true);
        settingsPanel.SetActive(false);

        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        if (GameManager.instance == null) return;

        int unlockedLevel = GameManager.instance.GetUnlockedLevel();
        Debug.Log($"[MainMenu] Updating Level Buttons. Unlocked Level: {unlockedLevel}");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] == null) 
            {
                Debug.LogWarning($"[MainMenu] Level Button {i} is NULL. Check Inspector!");
                continue;
            }

            int levelNum = i + 1; // 0 -> Level 1
            bool isUnlocked = levelNum <= unlockedLevel;

            // Enable/Disable click
            levelButtons[i].interactable = isUnlocked;

            // Visual feedback (Fade out if locked)
            Image btnImage = levelButtons[i].GetComponent<Image>();
            if (btnImage != null)
            {
                Color c = btnImage.color;
                c.a = isUnlocked ? 1f : 0.5f; // Full opacity if unlocked, faded if locked
                btnImage.color = c;
            }
            
            Debug.Log($"[MainMenu] Level {levelNum}: {(isUnlocked ? "UNLOCKED" : "LOCKED")}");
        }
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

    // ROADMAP LEVEL SELECTION 
    public void LoadLevel(int levelIndex)
    {
        string levelName = "Level" + levelIndex; 
        
        if (SceneLoader.instance != null)
            SceneLoader.instance.LoadScene(levelName);
        else
            SceneManager.LoadScene(levelName);
    }

    public void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.instance) 
        {
            // Set Menu Volume directly
            AudioManager.instance.SetMenuVolume(value);
            
            // Set Gameplay Volume slightly lower (e.g. 60% of menu volume)
            AudioManager.instance.SetGameplayVolume(value * 0.6f);
        }
    }
    
    public void OnSFXVolumeChanged(float value)
    {
        if (AudioManager.instance) AudioManager.instance.SetSFXVolume(value);
    }
}
