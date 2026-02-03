using UnityEngine;

public class UnsafeCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip dangerSound;
    [SerializeField] private ParticleSpawner particleSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            particleSpawner.Spawn(transform.position);
            AudioManager.Instance.PlaySFX(dangerSound);
            Destroy(gameObject);
        }
    }
}
