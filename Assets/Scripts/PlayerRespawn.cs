using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        // Store the initial position where the player starts the level
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Respawn()
    {
        // 1. Teleport the player back to the start
        transform.position = startPosition;

        // 2. Safely kill all momentum (Standard for Unity 6)
        if (rb != null)
        {
            // Reset linear movement (walking/falling)
            rb.linearVelocity = Vector2.zero;
            // Reset rotation speed
            rb.angularVelocity = 0f;
        }

        // 3. Reset the world state (Tiles, Collectibles, and the 'hasSafe' flag)
        if (GameManager.instance != null)
        {
            // This now triggers the ResetLevelState() logic we added earlier
            GameManager.instance.ResetTiles();
        }
    }
}