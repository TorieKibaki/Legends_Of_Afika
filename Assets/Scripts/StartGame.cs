using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startPanel;

    void Start()
    {
        Time.timeScale = 0f; // pause game
    }

    public void BeginGame()
    {
        Time.timeScale = 1f; // resume game
        startPanel.SetActive(false);

        // This returns focus to the game so the keyboard works immediately
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
}
