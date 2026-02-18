using UnityEngine;
using TMPro; // Include this if you are using TextMeshPro

public class Door : MonoBehaviour
{
    [Header("Effects")]
    public GameObject doorParticles;
    public AudioClip doorSound;

    [Header("UI Reference")]
    public GameObject messageUI; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance == null) return;

        // Check if player collected the item
        if (GameManager.instance.hasSafeCollectible)
        {
            OpenDoor();
        }
        else
        {
            ShowLockedMessage();
        }
    }

    void OpenDoor()
    {
        if (doorSound)
            AudioSource.PlayClipAtPoint(doorSound, transform.position);

        if (doorParticles)
            Instantiate(doorParticles, transform.position, Quaternion.identity);

        // Notify Level Controller to End Level
        LevelController lc = FindFirstObjectByType<LevelController>();
        if (lc != null) lc.OnDoorPassed();
    }

    void ShowLockedMessage()
    {
        if (messageUI != null)
        {
            messageUI.SetActive(true);
            Invoke("HideLockedMessage", 2f);
        }
    }

    void HideLockedMessage()
    {
        if (messageUI != null) messageUI.SetActive(false);
    }
}