using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Effects")]
    public GameObject doorParticles;
    public AudioClip doorSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        // Check if player has the collectible
        if (GameManager.instance.hasSafeCollectible)
        {
            // Play effects
            if (doorSound)
                AudioSource.PlayClipAtPoint(doorSound, transform.position);

            if (doorParticles)
                Instantiate(doorParticles, transform.position, Quaternion.identity);

            // Tell LevelController door was passed
            LevelController lc = FindFirstObjectByType<LevelController>();
            if (lc != null)
            {
                lc.OnDoorPassed();
            }
            else
            {
                Debug.LogWarning("LevelController not found!");
            }
        }
        else
        {
            // Door is locked
            Debug.Log($"Door is locked! hasSafeCollectible is: {GameManager.instance.hasSafeCollectible}");
        }
    }
}
