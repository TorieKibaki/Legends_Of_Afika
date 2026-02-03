using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelButtons; // assign buttons in Inspector
    public string[] sceneNames;   // assign corresponding scene names

    void Start()
    {
        int unlockedLevel = GameManager.instance.GetUnlockedLevel();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= unlockedLevel)
            {
                // Enable button
                levelButtons[i].interactable = true;
            }
            else
            {
                // Disable button for locked levels
                levelButtons[i].interactable = false;
            }

            int index = i; // needed for lambda
            levelButtons[i].onClick.AddListener(() => LoadLevel(sceneNames[index]));
        }
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
