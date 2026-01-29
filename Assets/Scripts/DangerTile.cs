using UnityEngine;

public class DangerTile : MonoBehaviour
{
    public ParticleSystem deathParticles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(deathParticles, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
