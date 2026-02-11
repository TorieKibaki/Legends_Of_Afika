using UnityEngine;
using TMPro; // Include this if you are using TextMeshPro

public class Door : MonoBehaviour
{
    [Header("Effects")]
    public GameObject doorParticles;
    public AudioClip doorSound;

    [Header("UI Reference")]
    public GameObject messageUI; // Drag your "PopUp" GameObject here in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        // Check if player has the collectible
        if (GameManager.instance.hasSafeCollectible)
        {
            OpenDoor();
        }
        else
        {
            // NEW: Show the message because they don't have the item
            ShowLockedMessage();
        }
    }

    void OpenDoor()
    {
        if (doorSound)
            AudioSource.PlayClipAtPoint(doorSound, transform.position);

        if (doorParticles)
            Instantiate(doorParticles, transform.position, Quaternion.identity);

        LevelController lc = FindFirstObjectByType<LevelController>();
        if (lc != null) lc.OnDoorPassed();
    }

    void ShowLockedMessage()
    {
        if (messageUI != null)
        {
            messageUI.SetActive(true);
            // Optional: Hide the message after 2 seconds
            Invoke("HideLockedMessage", 2f);
        }
        Debug.Log("You need to collect one Artefact");
    }

    void HideLockedMessage()
    {
        if (messageUI != null) messageUI.SetActive(false);
    }
}