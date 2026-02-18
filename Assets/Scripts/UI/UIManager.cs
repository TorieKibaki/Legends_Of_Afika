using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Intro Panel")]
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public Image introImage; 
    public float introDuration = 3f;

    [Header("Fact Panel")]
    public GameObject factPanel;
    public TextMeshProUGUI factText;
    public Image factImage; 
    public float factDuration = 5f;

    [Header("End Panel")]
    public GameObject endPanel;
    public TextMeshProUGUI endText;
    public float endDuration = 3f;

    [Header("Level Complete Panel")]
    public GameObject levelCompletePanel;
    public TextMeshProUGUI completionText;

    [Header("Pause Panel")]
    public GameObject pausePanel;
    private bool isPaused = false;

    // State management
    public bool IsInputLocked { get; private set; }
    private Coroutine activeCoroutine;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Mobile Controls")]
    public GameObject mobileInputContainer; // Drag the parent object of your Left/Right/Jump buttons here

    void Start()
    {
        HideAllPanels();
        CheckMobilePlatform();
    }

    private void CheckMobilePlatform()
    {
        bool isMobile = Application.isMobilePlatform;

        if (mobileInputContainer != null)
        {
            mobileInputContainer.SetActive(isMobile);
        }
    }

    void HideAllPanels()
    {
        if (introPanel != null) introPanel.SetActive(false);
        if (factPanel != null) factPanel.SetActive(false);
        if (endPanel != null) endPanel.SetActive(false);
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    // --- SEQUENTIAL PANELS ---

    public void ShowIntro(string[] texts, Sprite[] images = null, float[] customDurations = null)
    {
        ShowSequentialPanel(introPanel, introText, introImage, texts, images, customDurations, introDuration);
    }

    public void ShowFact(string[] texts, Sprite[] images = null, float[] customDurations = null)
    {
        ShowSequentialPanel(factPanel, factText, factImage, texts, images, customDurations, factDuration);
    }

    // --- END & COMPLETION ---

    public void ShowEnd(string text)
    {
        HideAllPanels();

        if (endPanel != null)
        {
            endPanel.SetActive(true);
            if (endText != null) endText.text = text;
            
            IsInputLocked = true;

            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(AutoCloseEndPanel(endDuration));
        }
    }

    public void ShowLevelComplete(string text)
    {
        HideAllPanels(); 
        
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            if (completionText != null) completionText.text = text;
            
            // Keep input locked
            IsInputLocked = true; 
        }
    }

    // --- HELPERS ---

    private void ShowSequentialPanel(GameObject panel, TextMeshProUGUI textComp, Image imageComp, string[] contents, Sprite[] sprites, float[] customDurations, float defaultDuration)
    {
        HideAllPanels();

        if (panel != null && contents.Length > 0)
        {
            panel.SetActive(true);
            IsInputLocked = true;

            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(PlaySequence(panel, textComp, imageComp, contents, sprites, customDurations, defaultDuration));
        }
    }

    IEnumerator PlaySequence(GameObject panel, TextMeshProUGUI textComp, Image imageComp, string[] contents, Sprite[] sprites, float[] customDurations, float defaultDuration)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            // Update Text
            if (textComp != null) textComp.text = contents[i];
            
            // Update Image
            if (imageComp != null && sprites != null && i < sprites.Length)
            {
                if (sprites[i] != null) 
                {
                    imageComp.sprite = sprites[i];
                    imageComp.gameObject.SetActive(true);
                }
            }

            // Duration
            float stepDuration = defaultDuration;
            if (customDurations != null && i < customDurations.Length && customDurations[i] > 0f)
            {
                stepDuration = customDurations[i];
            }

            yield return new WaitForSecondsRealtime(stepDuration);
        }

        if (panel != null) panel.SetActive(false);
        IsInputLocked = false;
        activeCoroutine = null;
    }
    
    IEnumerator AutoCloseEndPanel(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        
        if (endPanel != null) endPanel.SetActive(false);
        
        // Notify Level Controller
        LevelController lc = FindFirstObjectByType<LevelController>();
        if (lc != null)
        {
            lc.OnEndPanelClosed();
        }
        
        activeCoroutine = null;
    }

    // --- NAVIGATION ---

    public void OnHomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRoadmapButton()
    {
        Time.timeScale = 1f;
        if (GameManager.instance != null)
        {
            GameManager.instance.returnToRoadmap = true;
        }

        SceneManager.LoadScene("MainMenu");
    }

    // --- PAUSE ---

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
            IsInputLocked = true;
        }
        else
        {
            Time.timeScale = 1f;
            IsInputLocked = false;
        }
    }

    public void OnPauseButton() => TogglePause();
    public void OnResumeButton() => TogglePause();
}
