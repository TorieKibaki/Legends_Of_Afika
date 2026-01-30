using UnityEngine;

public class DisappearingTile : MonoBehaviour
{
    public float delay = 3f;
    private SpriteRenderer spriteRenderer;
    private Collider2D tileCollider;

    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Disappear", delay);
        }
    }

    void Disappear()
    {
        // Hide the tile and disable physics, but keep the script running!
        spriteRenderer.enabled = false;
        tileCollider.enabled = false;
    }

    // This is the new function we will call when the player dies
    public void ResetTile()
    {
        CancelInvoke(); // Stop it from disappearing if it was about to
        spriteRenderer.enabled = true;
        tileCollider.enabled = true;
    }
}