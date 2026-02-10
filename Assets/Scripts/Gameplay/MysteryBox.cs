using UnityEngine;
using System.Collections;

public class MysteryBox : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject collectibleObject; // Drop the actual Safe/Unsafe collectible prefab here (as a child usually)
    public float fadeDuration = 0.5f;

    [Header("Visuals")]
    public SpriteRenderer boxSprite;
    public GameObject revealParticles;
    public AudioClip revealSound;

    private bool isOpened = false;

    void Start()
    {
        // 1. Ensure the collectible is HIDDEN at start
        if (collectibleObject != null)
        {
            collectibleObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("MysteryBox: No collectible assigned!");
        }

        // 2. Ensure Box is visible
        if (boxSprite == null) boxSprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger once
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenBox();
        }
    }

    private void OpenBox()
    {
        isOpened = true;

        // 1. Play Sound
        if (revealSound != null)
        {
            AudioSource.PlayClipAtPoint(revealSound, transform.position);
        }

        // 2. Play Particles
        if (revealParticles != null)
        {
            Instantiate(revealParticles, transform.position, Quaternion.identity);
        }

        // 3. Start Animation/Fade
        StartCoroutine(RevealProcess());
    }

    IEnumerator RevealProcess()
    {
        // Fade out the box sprite nicely if possible
        if (boxSprite != null)
        {
            float timer = 0f;
            Color originalColor = boxSprite.color;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                boxSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            
            boxSprite.enabled = false; // Completely hide after fade
        }
        else
        {
            // Fallback if no sprite renderer found
            yield return new WaitForSeconds(0.1f);
        }

        // 4. ACTIVATE the real collectible
        if (collectibleObject != null)
        {
            collectibleObject.SetActive(true);
            
            // IMPORTANT: If the player is already standing there, the collectible's OnTriggerEnter might NOT fire immediately 
            // depending on physics engine timing. However, usually enabling a collider inside another collider triggers it next frame.
        }
        
        // Destroy this box script/object after a while to clean up? 
        // Or just leave it disabled.
        // Destroy(gameObject, 1f); 
    }
}
