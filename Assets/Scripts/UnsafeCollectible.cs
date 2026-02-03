using UnityEngine;

public class UnsafeCollectible : MonoBehaviour
{
    [Header("Effects")]
    public GameObject deathParticles;
    public AudioClip deathSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Play sound
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        // Play particles at player position
        if (deathParticles != null)
            Instantiate(deathParticles, other.transform.position, Quaternion.identity);

        // Respawn player
        other.GetComponent<PlayerRespawn>().Respawn();
    }
}
