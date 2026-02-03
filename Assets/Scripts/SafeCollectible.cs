using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private ParticleSpawner particleSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            particleSpawner.Spawn(transform.position);
            AudioManager.Instance.PlaySFX(collectSound);
            Destroy(gameObject);
        }
    }
}
