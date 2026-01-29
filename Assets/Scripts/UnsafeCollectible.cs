using UnityEngine;

public class UnsafeCollectible : MonoBehaviour
{
    public AudioClip deathSound;
    public ParticleSystem deathParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Instantiate(deathParticles, collision.transform.position, Quaternion.identity);
            collision.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
