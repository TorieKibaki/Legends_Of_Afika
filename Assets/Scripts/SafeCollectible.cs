using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    [Header("Gameplay")]
    public string funFact;

    [Header("Effects")]
    public GameObject collectParticles;
    public AudioClip collectSound;

    // References to keep track of state
    private Collider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        // Register this collectible with the GameManager so it can be reset
        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterCollectible(this); // You'll need to add this method to GameManager
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Mark collectible collected in the game logic
        GameManager.instance.CollectSafe();

        // Play sound
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        // Play particles
        if (collectParticles != null)
            Instantiate(collectParticles, transform.position, Quaternion.identity);

        // Show fun fact
        UIManager.instance.ShowFunFact(funFact);

        // INSTEAD OF DESTROY: Disable visuals and physics
        SetCollectibleState(false);
    }

    // This is the "Respawn" function
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