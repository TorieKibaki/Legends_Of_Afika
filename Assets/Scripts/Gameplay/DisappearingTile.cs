using UnityEngine;
using System.Collections;

public class DisappearingTile : MonoBehaviour
{
    private Vector3 startPosition;
    private Collider2D col;
    private SpriteRenderer sr;

    [SerializeField] private float disappearDelay = 2f; // Time player must stay on tile
    private float timer = 0f;
    private bool isPlayerOnTile = false;

    void Start()
    {
        startPosition = transform.position;
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterTile(this);
        }
    }

    void Update()
    {
        // Only count up if the player is currently standing on the tile
        if (isPlayerOnTile)
        {
            timer += Time.deltaTime;

            if (timer >= disappearDelay)
            {
                Disappear();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTile = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTile = false;
            timer = 0f; // Reset the timer so they have to start over
        }
    }

    void Disappear()
    {
        sr.enabled = false;
        col.enabled = false;
        isPlayerOnTile = false;
        timer = 0f;
    }

    public void ResetTile()
    {
        transform.position = startPosition;
        sr.enabled = true;
        col.enabled = true;
        timer = 0f;
        isPlayerOnTile = false;
    }
}