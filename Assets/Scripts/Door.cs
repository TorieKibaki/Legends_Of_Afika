using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public string nextSceneName;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;


        if (GameManager.instance.hasSafeCollectible)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            UIManager.instance.ShowHint("You need at least one artefact!");
        }
    }
}