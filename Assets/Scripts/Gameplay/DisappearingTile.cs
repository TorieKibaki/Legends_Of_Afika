using UnityEngine;
using System.Collections;

public class DisappearingTile : MonoBehaviour
{
    private Vector3 startPosition;
    private Collider2D col;
    private SpriteRenderer sr;
    private bool triggered = false;

    void Start()
    {
        startPosition = transform.position;
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        // Register this tile so the GameManager knows to reset it on player death
        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterTile(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // OnCollisionEnter2D is more efficient than Stay2D for a single trigger
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(DisappearRoutine());
        }
    }

    IEnumerator DisappearRoutine()
    {
        yield return new WaitForSeconds(5f);

        // We disable visuals and physics rather than the whole GameObject.
        // If we set the GameObject to inactive, this script stops running!
        sr.enabled = false;
        col.enabled = false;
    }

    // This is called by your GameManager.instance.ResetTiles()
    public void ResetTile()
    {
        StopAllCoroutines(); // Stop any pending disappearances

        transform.position = startPosition;
        sr.enabled = true;
        col.enabled = true;
        triggered = false;
    }
}