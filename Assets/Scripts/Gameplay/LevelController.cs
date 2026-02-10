using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Level Info")]
    public int levelIndex;

    [Header("Panel Content")]
    [TextArea(3, 10)]
    public string introText = "Welcome to the Level!";
    
    [TextArea(3, 10)]
    public string safeCollectibleFact = "Did you know? [Fact goes here]";

    [TextArea(3, 10)]
    public string endText = "You've reached the exit!";

    void Start()
    {
        // Show Intro Panel at start
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowIntro(introText);
        }
    }

    // Called by SafeCollectible when collected
    public void OnSafeCollected()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowFact(safeCollectibleFact);
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
