using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    [Header("Effects")]
    public GameObject collectParticles;
    public AudioClip collectSound;

    private Collider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        // Register with GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterCollectible(this); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Mark as collected
        if (GameManager.instance != null)
            GameManager.instance.CollectSafe();

        // Play effects
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (collectParticles != null)
            Instantiate(collectParticles, transform.position, Quaternion.identity);

        // Tell LevelController to show fact
        LevelController lc = FindFirstObjectByType<LevelController>();
        if (lc != null)
        {
            lc.OnSafeCollected();
        }

        // Hide collectible
        SetCollectibleState(false);
    }

    public void ResetCollectible()
    {
        SetCollectibleState(true);
    }

    private void SetCollectibleState(bool active)
    {
        if (sr != null) sr.enabled = active;
        if (col != null) col.enabled = active;
    }
}