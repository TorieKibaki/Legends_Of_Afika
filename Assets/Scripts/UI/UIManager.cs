using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Intro Panel")]
    public GameObject introPanel;
    public TextMeshProUGUI introText;
    public float introDuration = 3f;

    [Header("Fact Panel")]
    public GameObject factPanel;
    public TextMeshProUGUI factText;
    public float factDuration = 5f;

    [Header("End Panel")]
    public GameObject endPanel;
    public TextMeshProUGUI endText;
    public float endDuration = 3f;

    [Header("Level Complete Panel")]
    public GameObject levelCompletePanel;
    public TextMeshProUGUI completionText;

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
    public GameObject mobileControlsPanel;

    void Start()
    {
        // Hide all panels at start
        HideAllPanels();

        // Check for mobile platform
        CheckMobilePlatform();
    }

    private void CheckMobilePlatform()
    {
        bool isMobile = Application.isMobilePlatform;
        
        // Debugging in Editor: Uncomment next line to test mobile UI on PC
        // isMobile = true; 

        if (mobileControlsPanel != null)
        {
            mobileControlsPanel.SetActive(isMobile);
        }
    }

    void HideAllPanels()
    {
        if (introPanel != null) introPanel.SetActive(false);
        if (factPanel != null) factPanel.SetActive(false);
        if (endPanel != null) endPanel.SetActive(false);
        if (levelCompletePanel != null) levelCompletePanel.SetActive(false);
        
        // Does NOT automatically unlock input (caller decides)
        // If we want a hard reset of state, we could do:
        // IsInputLocked = false;
        // But usually we hide panels implicitly before showing another.
    }

    // --- INTRO PANEL ---
    public void ShowIntro(string text)
    {
        ShowTimedPanel(introPanel, introText, text, introDuration);
    }

    // --- FACT PANEL ---
    public void ShowFact(string text)
    {
        ShowTimedPanel(factPanel, factText, text, factDuration);
    }

    // --- END PANEL ---
    public void ShowEnd(string text)
    {
        // Close others first
        HideAllPanels();

        if (endPanel != null)
        {
            endPanel.SetActive(true);
            if (endText != null) endText.text = text;
            
            // Lock input
            IsInputLocked = true;

            // Stop existing coroutine if any (though HideAllPanels handles cleanup usually, 
            // we want to be sure we track this specific sequence)
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(AutoCloseEndPanel(endDuration));
        }
    }

    // --- LEVEL COMPLETE PANEL ---
    public void ShowLevelComplete(string text)
    {
        // Clean up previous states
        HideAllPanels(); 
        
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            if (completionText != null) completionText.text = text;
            
            // Keep input locked while in menu
            IsInputLocked = true; 
        }
    }

    // --- HELPER METHDOS ---

    private void ShowTimedPanel(GameObject panel, TextMeshProUGUI textComp, string content, float duration)
    {
        // 1. Hide any currently active panels to prevent overlap
        HideAllPanels();

        // 2. Setup new panel
        if (panel != null)
        {
            panel.SetActive(true);
            if (textComp != null) textComp.text = content;

            // 3. Set state
            IsInputLocked = true;

            // 4. Start timer (cancelling previous if valid)
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(AutoClosePanel(panel, duration));
        }
    }

    IEnumerator AutoClosePanel(GameObject panel, float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        
        if (panel != null) panel.SetActive(false);
        
        // Unlock input after panel closes
        IsInputLocked = false;
        activeCoroutine = null;
    }

    IEnumerator AutoCloseEndPanel(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        
        if (endPanel != null) endPanel.SetActive(false);
        
        // Do NOT unlock input yet, because we go straight to Level Complete
        // Trigger event that level is complete
        LevelController lc = FindFirstObjectByType<LevelController>();
        if (lc != null)
        {
            lc.OnEndPanelClosed();
        }
        
        activeCoroutine = null;
    }

    // --- BUTTON CALLBACKS ---
    public void OnHomeButton()
    {
        if (SceneLoader.instance != null)
            SceneLoader.instance.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void OnRoadmapButton()
    {
        // Set flag so Main Menu opens directly to Roadmap
        if (GameManager.instance != null)
        {
            GameManager.instance.returnToRoadmap = true;
        }

        if (SceneLoader.instance != null)
            SceneLoader.instance.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("MainMenu");
    }
}
