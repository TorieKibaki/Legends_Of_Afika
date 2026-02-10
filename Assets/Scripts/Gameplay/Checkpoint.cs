using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
               // set player position to this checkpoint position
                respawn.SetCheckpoint(transform.position);
                Debug.Log($"Checkpoint reached at: {transform.position}");
            }
        }
    }
}
