using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Header("Level Info")]
    public int levelIndex;

    [Header("Panel Content")]
    [TextArea(3, 10)]
    public string[] introSequence = new string[] { "Welcome to the Level!" };
    
    [TextArea(3, 10)]
    public string[] factSequence = new string[] { "Did you know? [Fact goes here]" };

    [Header("Panel Images")]
    public Sprite[] introSprites; // Ensure size matches text array
    public Sprite[] factSprites;  // Ensure size matches text array

    [Header("Panel Timing (Optional)")]
    public float[] introDurations; // Custom time for each slide. If empty, uses default.
    public float[] factDurations;

    [TextArea(3, 10)]
    public string endText = "You've reached the exit!";

    void Start()
    {
        // Show Intro Panel at start
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowIntro(introSequence, introSprites, introDurations);
        }
    }

    // Called by SafeCollectible when collected
    public void OnSafeCollected()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowFact(factSequence, factSprites, factDurations);
        }
    }

    // Called by Door when player passes through
    public void OnDoorPassed()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowEnd(endText);
        }
        
        // Unlock next level
        if (GameManager.instance != null)
        {
            GameManager.instance.UnlockLevel(levelIndex + 1);
        }
    }

    // Called by UIManager after End Panel closes
    public void OnEndPanelClosed()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowLevelComplete($"Level {levelIndex} Complete!");
        }
    }
}