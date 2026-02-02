using UnityEngine;

public class DisappearingTile : MonoBehaviour
{
    public float delay = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Invoke("Disappear", delay);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
