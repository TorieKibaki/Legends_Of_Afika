using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Next Level")]
    public string nextSceneName;

    [Header("Effects (Unlocked Only)")]
    public GameObject doorParticles;
    public AudioClip doorSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance.hasSafeCollectible)
        {
            if (doorSound != null)
                AudioSource.PlayClipAtPoint(doorSound, transform.position);

            if (doorParticles != null)
                Instantiate(doorParticles, transform.position, Quaternion.identity);

            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            UIManager.instance.ShowHint("You need at least one artefact!");
        }
    }
}
