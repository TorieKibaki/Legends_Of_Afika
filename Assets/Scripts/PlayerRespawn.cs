using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    [System.Obsolete]
    public void Respawn()
    {
        // 1. Move the player to your new starting position
        transform.position = new Vector3(-10f, -3f, 0f);

        // 2. Find and reset tiles (Updated for Unity 6)
        DisappearingTile[] allTiles = GameObject.FindObjectsByType<DisappearingTile>(FindObjectsSortMode.None);

        foreach (DisappearingTile tile in allTiles)
        {
            tile.ResetTile();
        }
    }
}
