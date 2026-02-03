using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    [Header("Gameplay")]
    public string funFact;

    [Header("Effects")]
    public GameObject collectParticles;
    public AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Mark collectible collected
        GameManager.instance.CollectSafe();

        // Play sound
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        // Play particles
        if (collectParticles != null)
            Instantiate(collectParticles, transform.position, Quaternion.identity);

        // Show fun fact
        UIManager.instance.ShowFunFact(funFact);

        Destroy(gameObject);
    }
}
