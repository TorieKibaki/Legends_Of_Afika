using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Respawn()
    {
        // 1. Move the player to the start position immediately
        transform.position = startPosition;

        // 2. Safely stop all physics movement
        if (rb != null)
        {
            // Stop moving (linear) and stop spinning (angular)
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 3. Reset environmental hazards (Disappearing tiles, etc.)
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetTiles();
        }
    }
}
