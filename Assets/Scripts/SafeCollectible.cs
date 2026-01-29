using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    public string funFact;
    public AudioClip collectSound;
    public ParticleSystem collectParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.CollectSafe(funFact);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Instantiate(collectParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
