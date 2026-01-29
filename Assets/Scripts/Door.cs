using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;
    public AudioClip doorSound;
    public ParticleSystem doorParticles;

    private bool isActive = false;

    public void ActivateDoor()
    {
        isActive = true;
        Instantiate(doorParticles, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            AudioSource.PlayClipAtPoint(doorSound, transform.position);
            SceneManager.LoadScene(nextScene);
        }
    }
}
