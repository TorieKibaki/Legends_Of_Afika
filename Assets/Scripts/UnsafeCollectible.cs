using UnityEngine;


public class UnsafeCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}