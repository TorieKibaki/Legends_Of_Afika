using UnityEngine;

public class SafeCollectible : MonoBehaviour
{
    [Header("Effects")]
    public GameObject collectParticles;
    public AudioClip collectSound;

    private Collider2D col;
    private SpriteRenderer sr;
    private bool wasCollected = false;

    [Header("Settings")]
    public float displayDuration = 0.5f;

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

    private Coroutine currentCollectRoutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        // Stop any previous routine just in case
        if (currentCollectRoutine != null) StopCoroutine(currentCollectRoutine);
        currentCollectRoutine = StartCoroutine(CollectSequence());
    }

    private System.Collections.IEnumerator CollectSequence()
    {
        // 1. Disable collider immediately to prevent double trigger
        if (col != null) col.enabled = false;

        // 2. Mark as collected in GM (Unlock door immediately)
        if (GameManager.instance != null)
            GameManager.instance.CollectSafe();

        // 3. Play effects
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (collectParticles != null)
            Instantiate(collectParticles, transform.position, Quaternion.identity);

        // 4. WAIT so player can see the item
        yield return new WaitForSeconds(displayDuration);

        // 5. Show Fact (only if first time)
        if (!wasCollected)
        {
            LevelController lc = FindFirstObjectByType<LevelController>();
            if (lc != null)
            {
                lc.OnSafeCollected();
            }
            wasCollected = true;
        }

        // 6. Finally Hide Sprite
        if (sr != null) sr.enabled = false;
        
        currentCollectRoutine = null;
    }

    public void ResetCollectible()
    {
        // Stop any running sequence so it doesn't hide the item after we respawn
        if (currentCollectRoutine != null) 
        {
            StopCoroutine(currentCollectRoutine);
            currentCollectRoutine = null;
        }

        // Always respawn the item so the player has to get it again
        SetCollectibleState(true);
    }

    private void SetCollectibleState(bool active)
    {
        if (sr != null) sr.enabled = active;
        if (col != null) col.enabled = active;
    }
}