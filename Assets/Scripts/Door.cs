using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextSceneName;
    public int levelToUnlock;

    public GameObject doorParticles;
    public AudioClip doorSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance.hasSafeCollectible)
        {
            if (doorSound)
                AudioSource.PlayClipAtPoint(doorSound, transform.position);

            if (doorParticles)
                Instantiate(doorParticles, transform.position, Quaternion.identity);

            //Save progress
            GameManager.instance.UnlockLevel(levelToUnlock);

            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            UIManager.instance.ShowHint("You need at least one artefact!");
        }
    }
}
