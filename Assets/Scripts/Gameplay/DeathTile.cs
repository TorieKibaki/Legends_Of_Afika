using UnityEngine;


public class DeathTile : MonoBehaviour
{
    public AudioClip deathSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (AudioManager.instance != null && deathSound != null)
            {
                AudioManager.instance.PlaySFX(deathSound);
            }
            collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}